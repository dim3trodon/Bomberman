using UnityEngine;
using System.Collections;

public class Jugador : ElementoTableroMovil {

	public Jugador(GameObject elemento):base(elemento) {}

	override
	public void Destruir() {
		GameObject.Destroy(Elemento);
	}

	override
	public bool EsEnemigo() {
		return false;
	}
}
