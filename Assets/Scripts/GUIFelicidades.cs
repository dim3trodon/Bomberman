using UnityEngine;
using System.Collections;

public class GUIFelicidades : MonoBehaviour {

	void OnGUI() {
		Rect rect = new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100);
		GUI.Label(rect, "¡Felicidades!");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
