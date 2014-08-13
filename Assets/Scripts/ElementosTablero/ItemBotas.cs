// Item que aumenta la velocidad del jugador.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public class ItemBotas : Item {

	public ItemBotas(GameObject elemento):base(elemento) {}

	override
	public void Obtener() {
		Control.AumentarVelocidadJugador();
		Destruir();
	}
}
