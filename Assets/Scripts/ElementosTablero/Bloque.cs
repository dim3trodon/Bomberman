using UnityEngine;
using System.Collections;

public class Bloque : ElementoTableroEstatico {

	override
	public void Destruir() {
		Debug.Log("Un bloque no se puede destruir");
	}

	override
	public bool EsDestruible() {
		return false;
	}

	public Bloque(GameObject elemento):base(elemento) {}
}
