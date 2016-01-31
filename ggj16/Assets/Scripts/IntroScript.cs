using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class IntroScript : MonoBehaviour {

		public AudioClip firstSentence;
		public AudioClip secondSentence;
		public AudioClip thirdSentence;
		
		private GameObject panel;

		public Sprite[] images; 
		
		void Start()
		{
			GetComponent<AudioSource> ().loop = false;
			panel =  GameObject.Find("Panel");
			StartCoroutine(playEngineSound());

		}

		IEnumerator playEngineSound()
		{
			GetComponent<AudioSource> ().clip = firstSentence;
			GetComponent<AudioSource> ().Play();
			yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length * 2/3);
			Image img = GameObject.Find ("Panel").GetComponent<Image>();
			img.sprite = images [3];		
			yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length * 1/3);
			GetComponent<AudioSource> ().clip = secondSentence;
			GetComponent<AudioSource> ().Play();
			yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length * 2/3);
			img.sprite = images [2];
			yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length * 1/3);
			GetComponent<AudioSource> ().clip = thirdSentence;
			GetComponent<AudioSource> ().Play();
			yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length * 2/3);
			img.sprite = images [3];
			yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length * 1/3);
			Application.LoadLevel ("Game");
		}
}
