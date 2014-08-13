// Item que aumenta el numero de bombas maximas que puede llevar el jugador.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public class ItemBomba : Item {

	public ItemBomba(GameObject elemento):base(elemento) {}

	override
	public void Obtener() {
		Control.AumentarBombas();
		Destruir();
	}
}
