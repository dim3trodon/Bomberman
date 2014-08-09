using UnityEngine;
using System.Collections;

public class Personaje : MonoBehaviour {

	private int x;
	public int X {
		get {
			return x;
		}
		set {
			x = value;
		}
	}
	private int xFinal;

	private int z;
	public int Z {
		get {
			return z;
		}
		set {
			z = value;
		}
	}
	private int zFinal;

	public const float velocidad = 3.0f;
	private float horaInicio;
	private float longCamino;

	private bool moviendose = false;
	private bool teclaPulsada = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!moviendose) {
			if (Input.GetKey (KeyCode.LeftArrow)){
				xFinal = x - 1;
				horaInicio = Time.time;
				teclaPulsada = true;
			}else if ((Input.GetKey (KeyCode.RightArrow) && !teclaPulsada)){
				xFinal = x + 1;
				horaInicio = Time.time;
				teclaPulsada = true;
			}else if (Input.GetKey (KeyCode.UpArrow) && !teclaPulsada){
				zFinal = z - 1;
				horaInicio = Time.time;
				teclaPulsada = true;
			}else if (Input.GetKey (KeyCode.DownArrow) && !teclaPulsada){
				zFinal = z + 1;
				horaInicio = Time.time;
			}
			teclaPulsada = false;
			if((x != xFinal) || (z != zFinal)) {
				// Si hay un obstaculo, no moverse
				if(Escenario.HayObstaculo(zFinal, xFinal)) {
					xFinal = x;
					zFinal = z;
				} else {
					moviendose = true;
				}
			}

		} else {
			float distCovered = (Time.time - horaInicio) * velocidad;
			transform.position = Vector3.Lerp(Escenario.GetPosicionReal(x, z),
			                                  Escenario.GetPosicionReal(xFinal, zFinal),
			                                  distCovered);
			if(transform.position == Escenario.GetPosicionReal(xFinal, zFinal)) {
				x = xFinal;
				z = zFinal;
				moviendose = false;
			}
		}
	}

	public void SetPosicionInicial(int x, int z) {
		X = x;
		Z = z;
		xFinal = x;
		zFinal = z;
	}
}
