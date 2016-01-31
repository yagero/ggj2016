using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class Ending : MonoBehaviour {

	public AudioClip ending;

	// Use this for initialization
	void Start () {

		StartCoroutine(playEngineSound());

	
	}
	
	// Update is called once per frame
	IEnumerator playEngineSound()
	{
		GetComponent<AudioSource> ().clip = ending;
		GetComponent<AudioSource> ().Play();
		yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length + 1);
		Application.LoadLevel ("MainMenu");
	}
}
