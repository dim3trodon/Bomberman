// Clase que mueve al jugador.
// Permite moverse en las cuatro direcciones (no en diagonal) y poner 
// una bomba.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public class MovimientoJugador : Movimiento {

	// Se usa para evitar el movimiento en diagonal
	private bool teclaPulsada = false;

	// Comprueba si hay un enemigo, un item o una puerta en la casilla actual del
	// jugador y si no, le permite moverse.
	void Update () {
		if(Control.HayEnemigoEn(I, J)) {
			Control.FinDelJuego();
		} else if(Control.HayItemEn(I, J)) {
			Control.ObtenerItemDe(I, J);
		} else if(Control.SePuedePasarDeFase() && Control.HayPuertaEn(I, J)) {
			Control.SiguienteFase();
		} else if(!moviendose) {
			if(Input.GetKey (KeyCode.Space)) {
				Control.PonerBomba(i, j);
			}else if (Input.GetKey (KeyCode.LeftArrow) && !teclaPulsada){
				jFinal = j - 1;
				horaInicio = Time.time;
				teclaPulsada = true;
			}else if ((Input.GetKey (KeyCode.RightArrow) && !teclaPulsada)){
				jFinal = j + 1;
				horaInicio = Time.time;
				teclaPulsada = true;
			}else if (Input.GetKey (KeyCode.UpArrow) && !teclaPulsada){
				iFinal = i - 1;
				horaInicio = Time.time;
				teclaPulsada = true;
			}else if (Input.GetKey (KeyCode.DownArrow) && !teclaPulsada){
				iFinal = i + 1;
				horaInicio = Time.time;
			}
			teclaPulsada = false;
			if((j != jFinal) || (i != iFinal)) {
				// Si hay un obstaculo, no moverse (iFinal y jFinal vuelve
				// a ser la posicion actual del jugador)
				if(Control.HayObstaculoEn(iFinal, jFinal)) {
					jFinal = j;
					iFinal = i;
				} else {
					moviendose = true;
				}
			}
		} else {
			Lerp();
		}
	}
}
