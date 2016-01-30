using UnityEngine;
using System.Collections;

public class CameraShader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera.main.SetReplacementShader (Shader.Find ("Toon/Basic Outline"), "RenderType");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
