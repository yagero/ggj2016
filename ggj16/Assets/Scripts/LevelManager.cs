using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {


	private GameObject panel;

	void Start(){
		panel =  GameObject.Find("Credits");
	
	}

	public void loadScene(string name){
		Application.LoadLevel (name);
	}

	public void openPanel(string name){
		Debug.Log (name);
		panel =  GameObject.Find(name);

		panel.GetComponent<CanvasGroup>().alpha = 1;
		panel.GetComponent<CanvasGroup>().interactable = true;
		panel.GetComponent<CanvasGroup>().blocksRaycasts = true;

		//GetComponent< Renderer >().enabled = false;
	}

	public void closePanel(string name){
		panel =  GameObject.Find(name);

		panel.GetComponent<CanvasGroup>().alpha = 0;
		panel.GetComponent<CanvasGroup>().interactable = false;
		panel.GetComponent<CanvasGroup>().blocksRaycasts = false;

	}
}
