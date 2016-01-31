using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class GameManager : Singleton<GameManager>
{
	//sounds
	public AudioClip first;
	public AudioClip second;
	public AudioClip third;
	public AudioClip last;

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
    }

    public void PickUpItem(RhythmGame rhythmGame)
    {
        ItemPickedCount++;

        if (ItemPickedCount >= TotalItemsToUnlockThePot)
        {
            UnlockThePot();
			GetComponent<AudioSource> ().clip = last;
			GetComponent<AudioSource> ().Play();
        }

		if (ItemPickedCount == 2) {
			GetComponent<AudioSource> ().clip = second;
			GetComponent<AudioSource> ().Play();
		}

		if (ItemPickedCount == 3) {
			GetComponent<AudioSource> ().clip = third;
			GetComponent<AudioSource> ().Play();
		}
			

        if (rhythmGame.gameObject == ThePot.gameObject)
        {
            GameWon();
        }
    }

    private void UnlockThePot()
    {
        ThePot.EnableGameplay();
    }

    private void GameWon()
    {
        // PUT GAME WON CODE HERE
        print("GAME WON!");
    }
}
