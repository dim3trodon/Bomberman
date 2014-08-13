// Caja del tablero. Puede ser destruida por una bomba.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public class Caja : ElementoTableroEstatico {

	public const string CajaString = "Caja";

	public Caja(GameObject elemento):base(elemento) {}

	override
	public void Destruir() {
		GameObject.Destroy(Elemento);
	}

	override
	public bool EsDestruible() {
		return true;
	}

	override
	public string ToString() {
		return CajaString;
	}
}
