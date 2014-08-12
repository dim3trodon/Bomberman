using UnityEngine;
using System.Collections;

public class MovimientoJugador : Movimiento {

	// Se usa para evitar el movimiento en diagonal
	private bool teclaPulsada = false;

	// Update is called once per frame
	void Update () {
		if(Control.HayEnemigoEn(X, Z)) {
			Control.FinDelJuego();
		} else if(Control.HayItemEn(X, Z)) {
			Control.ObtenerItemDe(X, Z);
		} else if(!moviendose) {
			if(Input.GetKey (KeyCode.Space)) {
				Control.PonerBomba(x, z);
				//teclaPulsada = true;
			}else if (Input.GetKey (KeyCode.LeftArrow) && !teclaPulsada){
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
				// Si hay un obstaculo, no moverse (xFinal y zFinal vuelve
				// a ser la posicion actual del jugador)
				if(Control.HayObstaculoEn(xFinal, zFinal)) {
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
