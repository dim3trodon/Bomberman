using UnityEngine;
using System.Collections;

public class MovimientoJugador : Movimiento {

	/*// Use this for initialization
	void Start () {

	}*/

	// Se usa para evitar el movimiento en diagonal
	private bool teclaPulsada = false;

	// Update is called once per frame
	void Update () {
		if(!moviendose) {
			if (Input.GetKeyDown (KeyCode.LeftArrow)){
				xFinal = x - 1;
				horaInicio = Time.time;
				teclaPulsada = true;
			}else if ((Input.GetKeyDown (KeyCode.RightArrow) && !teclaPulsada)){
				xFinal = x + 1;
				horaInicio = Time.time;
				teclaPulsada = true;
			}else if (Input.GetKeyDown (KeyCode.UpArrow) && !teclaPulsada){
				zFinal = z - 1;
				horaInicio = Time.time;
				teclaPulsada = true;
			}else if (Input.GetKeyDown (KeyCode.DownArrow) && !teclaPulsada){
				zFinal = z + 1;
				horaInicio = Time.time;
			}
			teclaPulsada = false;
			if((x != xFinal) || (z != zFinal)) {
				// Si hay un obstaculo, no moverse (xFinal y zFinal vuelve
				// a ser la posicion actual del jugador)
				if(Control.HayObstaculoEn(xFinal, zFinal)) {
					Debug.Log("No mover -------------------------------------------------");
					xFinal = x;
					zFinal = z;
				} else {
					Debug.Log("Mover ----------------------------------------------------");
					moviendose = true;
				}
			}
		} else {
			Lerp();
		}
	}
}
