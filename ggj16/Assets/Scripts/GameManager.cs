using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
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
    }

    public void PickUpItem(RhythmGame rhythmGame)
    {
        ItemPickedCount++;

        if (ItemPickedCount >= TotalItemsToUnlockThePot)
        {
            UnlockThePot();
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
