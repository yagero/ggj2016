using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class IntroScript : MonoBehaviour {

		public AudioClip firstSentence;
		public AudioClip secondSentence;
		public AudioClip thirdSentence;
		void Start()
		{
			GetComponent<AudioSource> ().loop = false;
			StartCoroutine(playEngineSound());
		}

		IEnumerator playEngineSound()
		{
			GetComponent<AudioSource> ().clip = firstSentence;
			GetComponent<AudioSource> ().Play();
			yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length);
			GetComponent<AudioSource> ().clip = secondSentence;
			GetComponent<AudioSource> ().Play();
			yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length);
			GetComponent<AudioSource> ().clip = thirdSentence;
			GetComponent<AudioSource> ().Play();
			yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length);
			Application.LoadLevel ("Game");
		}
}
