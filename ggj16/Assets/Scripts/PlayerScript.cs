using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int speed = 10;
	public float speedH = 2.0f;
	public float speedV = 2.0f;

	private float yaw = 0.0f;
	private float pitch = 0.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("up") || Input.GetKey ("w")) {
			var tZ = Time.deltaTime * speed;
			transform.Translate(0,0,tZ);
		}
		if (Input.GetKey ("down") || Input.GetKey ("s")) {
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

		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
	}
}
