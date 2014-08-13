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
	public bool ParaAvanceExplosion() {
		return true;
	}
	
	override
	public void Obtener() {
		Debug.Log("Un " + ToString() + " no se puede obtener");
	}
}
