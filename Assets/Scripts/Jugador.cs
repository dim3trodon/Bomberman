using UnityEngine;
using System.Collections;

public class Jugador : Personaje {

	private bool teclaPulsada = false;
	
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
			Lerp();
		}
	}
}
