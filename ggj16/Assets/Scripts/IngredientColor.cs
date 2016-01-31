﻿using UnityEngine;
using System.Collections;

public class IngredientColor : MonoBehaviour {
	Color color = Color.black;
	GameObject player;
	float distance;
	string itemName;

	bool colorChanging;

	// Use this for initialization
	void Start () {
		color.g = 185f;
		color.r = 185f;
		color.b = 185f;
		//calculateDistance ();
		colorChanging = false;


		Debug.Log(name);

		player = GameObject.FindGameObjectWithTag("MainCamera");
	}

	private float lastdistance = 0;

	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(player.transform.position, transform.position);

		if (distance < lastdistance) { 
			if (colorChanging == false) {	

				StartCoroutine(changeColorBright ());
				colorChanging = true;
			}
		} else if (distance > lastdistance) {
			if (colorChanging == false) {	

				StartCoroutine(changeColorDark ());
				colorChanging = true;
			}
		}
		lastdistance = distance; // don't forget to update last distance! 


	}


	IEnumerator changeColorBright(){
		color.g = color.g - (float)5f;
		color.b = color.b - (float)5f;
		Debug.Log (color.g);
		GetComponent<Renderer> ().material.color = new Color (color.r/255, color.g/255, color.b/255);
		yield return new WaitForSeconds (0.5f);
		colorChanging = false;
	}

	IEnumerator changeColorDark(){
		color.g = color.g + (float)5f;
		color.b = color.b + (float)5f;
		GetComponent<Renderer> ().material.color = new Color (color.r/255, color.g/255, color.b/255);
		yield return new WaitForSeconds (0.5f);
		colorChanging = false;
	}



}