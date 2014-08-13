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
