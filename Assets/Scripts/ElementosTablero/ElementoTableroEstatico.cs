// Clase abstracta de la que heredan todos los elementos estaticos del tablero.
// Todos los elementos estaticos del tablero son obstaculos, no hacen daño 
// al ser tocados por el usuarios, paran el avance de la llama y no pueden
// ser obtenidos.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public abstract class ElementoTableroEstatico : ElementoTablero {

	public ElementoTableroEstatico(GameObject elemento):base(elemento) {}

	override
	public bool EsObstaculo() {
		return true;
	}

	override
	public bool EsEnemigo() {
		return false;
	}

	override
	public bool EsObtenible() {
		return false;
	}

	override
	public bool ParaAvanceLlama() {
		return true;
	}
	
	override
	public void Obtener() {
		Debug.Log("Un " + ToString() + " no se puede obtener");
	}
}
