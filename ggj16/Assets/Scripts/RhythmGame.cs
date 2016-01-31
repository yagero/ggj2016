using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent (typeof (AudioSource))]
[RequireComponent (typeof (Collider))]
public class RhythmGame : MonoBehaviour
{

 //   public KeyframeDatabase m_KeyframeDatabase;

    public int ItemId;
    public bool EnabledAtStart = true;

    public KeyList m_KeyList;
    public float m_TimeAllowance = 300f;

    [HideInInspector]
    public int m_MaxTimeMultiplier = 1;

    private AudioSource m_AudioSource;
    private Light m_Light;
    private Slider m_Slider;
    private float m_PreviousTime = 0f;
    private float m_LightStartIntensity;
    private float m_AudioSourceStartingPitch;
    private KeyList.Key m_CurrentKey = null;
    private bool m_HasFailedInThisRun = false;

    //private ParticleSystem m_ParticleSystem;

    private int m_TimeMultiplier;

    private GameObject m_KeysLayer;
    private float m_TimeLineWidth;
    private bool m_IsPlaying = false;
    private bool m_GameplayEnabled = true;


	// Use this for initialization
	void Start ()
    {
        m_KeysLayer = new GameObject();
        m_KeysLayer.transform.SetParent(GameManager.Instance.FullTimeline.transform);
        m_KeysLayer.transform.localPosition = new Vector2(GameManager.Instance.FullTimeline.rect.x, 0f);
        m_KeysLayer.SetActive(false);


        m_CurrentKey = null;

        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.clip = m_KeyList.AudioClip;
        m_AudioSource.loop = true;

        m_GameplayEnabled = EnabledAtStart;

        if(m_GameplayEnabled)
            m_AudioSource.Play();
        //m_ParticleSystem = GetComponentInChildren<ParticleSystem>();


        m_Light = GetComponentInChildren<Light>();
        if (m_Light)
            m_LightStartIntensity = m_Light.intensity;

        m_AudioSourceStartingPitch = m_AudioSource.pitch;

        //   m_Slider = GameManager.Instance.HUD.GetComponentInChildren<Slider>();
        GameManager.Instance.HUD.SetActive(false);
       

        foreach(KeyList.Key key in m_KeyList.Keys)
        {
            SetSliderTime(key.TimeInMillisec);
          //  print(m_DebugSlider.handleRect.position);

            var marker = GameObject.Instantiate(GameManager.Instance.SliderTimerMarkerGAO);

            var textComp = marker.GetComponentInChildren<Text>();
            if (textComp)
            {
                var keyString = key.KeyCode.ToString();
                // Remove "Alpha"
                string removeString = "Alpha";
                int index = keyString.IndexOf(removeString);
                keyString = (index < 0)
                    ? keyString
                    : keyString.Remove(index, removeString.Length);
                textComp.text = keyString;
            }

            marker.transform.SetParent(m_KeysLayer.transform);

            float relativeTime = key.TimeInMillisec / GameManager.Instance.TimeLineMillisec;
            relativeTime *= GameManager.Instance.FullTimeline.rect.width;
            marker.transform.localPosition = new Vector2(relativeTime, 0f);

            key.SliderMarkerImage = marker.GetComponent<Image>();
        }

        m_TimeLineWidth = GameManager.Instance.FullTimeline.rect.width;
	}

    public void EnableGameplay()
    {
        m_GameplayEnabled = true;
        if (m_AudioSource)
            m_AudioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!m_GameplayEnabled)
            return;
        
        if(other.gameObject.CompareTag("Player"))
        {
            m_AudioSource.time = 0f;

            StartGameplay();
        //    StopGameplay(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!m_GameplayEnabled)
            return;
        
        if(other.gameObject.CompareTag("Player"))
        {
            StopGameplay(false);
        }
    }

    void StartGameplay()
    {
        m_PreviousTime = m_AudioSource.time;
        m_IsPlaying = true;

        GameManager.Instance.HUD.SetActive(true);
        m_KeyList.Reset();
        m_KeysLayer.SetActive(true);
      //  m_AudioSource.Play();

        //m_ParticleSystem.Emit(0);
        //SetParticleEmissionRate(0f);

        SetTimeMultiplier(1);
        StartCoroutine(Gameplay());

      //  StopGameplay(true); // DEBUG STUFF
    }

    void StopGameplay(bool won)
    {
        StopAllCoroutines();
      //  m_AudioSource.Stop();
        //m_ParticleSystem.Stop();

        m_KeysLayer.SetActive(false);
        GameManager.Instance.HUD.SetActive(false);

        m_IsPlaying = false;

        if (won)
        {
         //   m_ParticleSystem.Emit(10);
            GameManager.Instance.PickUpItem(this);
            GameObject.Destroy(gameObject);
        }
    }

    IEnumerator Gameplay()
    {
        m_HasFailedInThisRun = false;

        int keyId = 0;
        while(keyId < m_KeyList.Keys.Length)
        {
            var key = m_KeyList.Keys[keyId];

            float keyTime = key.TimeInMillisec;
            if (keyTime < GetAudioClipLengthMS())
            {
                yield return StartCoroutine(WaitForNextTime(key.TimeInMillisec - m_TimeAllowance / 2));

                m_CurrentKey = key;

                yield return StartCoroutine(WaitForNextTime(key.TimeInMillisec + m_TimeAllowance / 2));

                CheckIfFailed();

                m_CurrentKey = null;
            }
                
            keyId++;
        }

        yield return null;
    }

    private void CheckIfFailed()
    {
        if (m_CurrentKey != null)
        {
            if (m_CurrentKey.CheckIfFailed())
            {
                // Has Failed
                m_HasFailedInThisRun = true;

                if(m_Light)
                    m_Light.intensity = m_LightStartIntensity;

                SetTimeMultiplier(1);
                m_AudioSource.pitch = m_AudioSourceStartingPitch;
            }
           
        }
    }

    IEnumerator WaitForNextTime(float time)
    {
        while (GetAudioClipCurrentMS() < time)
        {
            if (m_CurrentKey != null && m_CurrentKey.Succeed)
                    break;

            yield return null;
        }
            
        yield return null;
    }


//    void SetParticleEmissionRate(float value)
//    {
//        if (m_ParticleSystem)
//        {
//            var em = m_ParticleSystem.emission;
//            var rate = new ParticleSystem.MinMaxCurve(value);
//            em.rate = rate;
//        }
//    }

    float GetAudioClipCurrentMS()
    {
        return m_AudioSource.time * 1000f;
    }

    float GetAudioClipLengthMS()
    {
        return m_AudioSource.clip.length * 1000f;
    }


    bool CanTouch()
    {
        return m_CurrentKey != null;
    }

    void SetSliderTime(float ms)
    {
     //   m_Slider.value = ms / GetAudioClipLengthMS();
    }

    void SetTimeMultiplier(int value)
    {
        m_TimeMultiplier = value;

        if(GameManager.Instance.TimerMultiplierText != null)
            GameManager.Instance.TimerMultiplierText.text = "x" + value;
    }


    void OnMusicRestart()
    {
        StopAllCoroutines();

        m_KeyList.Reset();


        if (!m_HasFailedInThisRun)
        {
            m_AudioSource.pitch *= 1.05f;
            SetTimeMultiplier(m_TimeMultiplier + 1);
        }

        if (m_HasFailedInThisRun || m_TimeMultiplier < m_MaxTimeMultiplier)
        {
            StartCoroutine(Gameplay());
        }
        else
        {
            StopGameplay(true);
        }
    }



	
	// Update is called once per frame
	void Update ()
    {
        if (m_IsPlaying)
        {
            SetSliderTime(GetAudioClipCurrentMS());
            float relTime = GetAudioClipCurrentMS() / GameManager.Instance.TimeLineMillisec;
            relTime *= m_TimeLineWidth;

            var pos = new Vector3(GameManager.Instance.TimeLineOffset.transform.localPosition.x - relTime, 0f, 0f);
            m_KeysLayer.transform.localPosition = pos;

            if (CanTouch())
            {
                if (Input.GetKeyDown(m_CurrentKey.KeyCode))
                {
                    m_CurrentKey.MarkAsSucceeed();
                }
            }

            if (!m_HasFailedInThisRun)
            {
                if (m_Light)
                    m_Light.intensity += 0.1f * Time.deltaTime;

                //            float constantValue = m_ParticleSystem.emission.rate.constantMin;
                //            SetParticleEmissionRate(m_ParticleSystem.emission.rate.constantMin + 100f * Time.deltaTime);
            }


            if (m_PreviousTime > m_AudioSource.time)
            {
                OnMusicRestart();
            }
        }
            
        m_PreviousTime = m_AudioSource.time;
	}
}
