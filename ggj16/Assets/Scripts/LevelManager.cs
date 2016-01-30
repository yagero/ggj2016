using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void loadScene(string name){
		Application.LoadLevel (name);
	}
}
