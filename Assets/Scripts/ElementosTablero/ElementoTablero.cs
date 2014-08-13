// Clase abstracta de la que heredan todos los elementos del tablero.
// Los elementos del tablero necesitan de un GameObject para tener 
// presencia visual en el tablero. Esto es Elemento.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public abstract class ElementoTablero {

	private GameObject elemento;
	public GameObject Elemento {
		get {
			return elemento;
		}
		set {
			elemento = value;
		}
	}

	public ElementoTablero(GameObject elemento) {
		Elemento = elemento;
	}

	public abstract void Destruir();

	public abstract bool EsObstaculo();

	public abstract bool EsDestruible();

	public abstract bool EsEnemigo();

	public abstract bool ParaAvanceLlama();

	public abstract bool EsObtenible();

	public abstract void Obtener();
}
