using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GameManager : Singleton<GameManager>
{
	//Cauldron panel
	private GameObject panel;


	//sounds
	public AudioClip first;
	public AudioClip second;
	public AudioClip third;
	public AudioClip last;

    public bool DevPickUpItemsImmediately = false;

    public GameObject SliderTimerMarkerGAO;
    public GameObject HUD;
    public RectTransform FullTimeline;
    public float TimeLineMillisec = 3000f;
    public RectTransform TimeLineOffset;

    [HideInInspector]
    public Text TimerMultiplierText;
    public RhythmGame ThePot;

    public int TotalItemsToUnlockThePot = 4;


    private int ItemPickedCount = 0;
        


    public void Start()
    {
        // Disable the pot GamePlay
		GetComponent<AudioSource> ().clip = first;
		GetComponent<AudioSource> ().Play();

		panel =  GameObject.Find("Cauldron");
    }

    public void PickUpItem(RhythmGame rhythmGame)
    {
        ItemPickedCount++;

        bool gameIsWon = (rhythmGame.gameObject == ThePot.gameObject);

        if (ItemPickedCount >= TotalItemsToUnlockThePot && !gameIsWon)
        {
			StartCoroutine(UnlockThePot());
        }

		if (ItemPickedCount == 2) {
			GetComponent<AudioSource> ().clip = second;
			GetComponent<AudioSource> ().Play();
		}

		if (ItemPickedCount == 3) {
			GetComponent<AudioSource> ().clip = third;
			GetComponent<AudioSource> ().Play();
		}

        if (gameIsWon)
        {
			StartCoroutine(GameWon());
        }
    }

	private IEnumerator UnlockThePot()
    {
		panel.GetComponent<CanvasGroup>().alpha = 1;
		panel.GetComponent<CanvasGroup>().interactable = true;
		panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        ThePot.EnableGameplay();
		yield return new WaitForSeconds(2);
		panel.GetComponent<CanvasGroup>().alpha =0;
		panel.GetComponent<CanvasGroup>().interactable = false;
		panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

	private IEnumerator GameWon()
    {
        // PUT GAME WON CODE HERE
        print("GAME WON!");
		GetComponent<AudioSource> ().clip = last;
		GetComponent<AudioSource> ().Play();
		yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length + 1);
		Application.LoadLevel ("Ending");
    }
}
