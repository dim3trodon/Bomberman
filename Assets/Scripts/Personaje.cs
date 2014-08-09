using UnityEngine;
using System.Collections;

public class Personaje : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)){
			transform.Translate(Vector3.left);
		}else if (Input.GetKeyDown (KeyCode.RightArrow)){
			transform.Translate(Vector3.right);
		}else if (Input.GetKeyDown (KeyCode.UpArrow)){
			transform.Translate(Vector3.forward);
		}else if (Input.GetKeyDown (KeyCode.DownArrow)){
			transform.Translate(Vector3.back);
		}
	}
}
