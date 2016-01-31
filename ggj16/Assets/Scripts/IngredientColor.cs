using UnityEngine;
using System.Collections;

public class IngredientColor : MonoBehaviour {
	public Color color = Color.black;
	GameObject player;
	float distance;


	// Use this for initialization
	void Start () {
		color.g = 185f;
		color.r = 185f;
		color.b = 185f;
		//calculateDistance ();

		player = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(player.transform.position, transform.position);

	}


	void changeColor(){

		while (distance < 100) {
			color.g = color.g - (float)2f;
			color.b = color.b - (float)2f;
		}

		GetComponent<Renderer>().material.color = new Color (color.r,color.g,color.b);
	}
}
