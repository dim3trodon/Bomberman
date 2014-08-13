// Bloque del tablero. No puede ser destruido por una bomba.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public class Bloque : ElementoTableroEstatico {

	public Bloque(GameObject elemento):base(elemento) {}

	override
	public void Destruir() {
		Debug.Log("Un bloque no se puede destruir");
	}

	override
	public bool EsDestruible() {
		return false;
	}
}
