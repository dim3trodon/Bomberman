using UnityEngine;
using System.Collections;

public class Explosion : ElementoTablero {

	public Explosion(GameObject elemento):base(elemento) {}

	override
	public void Destruir() {
		GameObject.Destroy(Elemento);
	}
	
	override
	public bool EsObstaculo() {
		return false;
	}
	
	override
	public bool EsDestruible() {
		return true;
	}
	
	override
	public bool EsEnemigo() {
		return true;
	}

	override
	public bool ParaAvanceExplosion() {
		return false;
	}

	override
	public bool EsObtenible() {
		return false;
	}

	override
	public void Obtener() {
		Debug.Log("Un " + ToString() + " no se puede obtener");
	}

}
