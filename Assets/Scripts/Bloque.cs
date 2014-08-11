using UnityEngine;
using System.Collections;

public class Bloque : ElementoTablero {

	override
	public void Destruir() {
		Debug.Log("Un bloque no se puede destruir");
	}

	override
	public bool EsObstaculo() {
		return true;
	}

	override
	public bool EsDestruible() {
		return false;
	}

	override
	public bool EsEnemigo() {
		return false;
	}

	public Bloque(GameObject elemento):base(elemento) {}
}
