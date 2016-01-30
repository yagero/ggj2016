using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int speed = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("up")) {
			var tZ = Time.deltaTime * speed;
			transform.Translate(0,0,tZ);
		}
		if (Input.GetKey ("down")) {
			var tZ = Time.deltaTime * speed;
			transform.Translate(0,0,-tZ);
		}
		if (Input.GetKey ("left")) {
			var tZ = Time.deltaTime * speed * 2;
			transform.Rotate(0,-tZ,0);
		}
		if (Input.GetKey ("right")) {
			var tZ = Time.deltaTime * speed * 2;
			transform.Rotate(0,tZ,0);
		}
	}
}
