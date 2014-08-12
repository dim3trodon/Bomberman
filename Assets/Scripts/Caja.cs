using UnityEngine;
using System.Collections;

public class Caja : ElementoTablero {

	override
	public void Destruir() {
		GameObject.Destroy(Elemento);
	}

	override
	public bool EsObstaculo() {
		return true;
	}

	override
	public bool EsDestruible() {
		return true;
	}

	override
	public bool EsEnemigo() {
		return false;
	}

	override
	public bool ParaAvanceExplosion() {
		return true;
	}

	public Caja(GameObject elemento):base(elemento) {}
}
