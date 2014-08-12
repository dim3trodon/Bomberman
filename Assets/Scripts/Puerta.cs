using UnityEngine;
using System.Collections;

public class Puerta : ElementoTablero {

	public Puerta(GameObject elemento):base(elemento) {

	}

	//private IEnumerator Comprobar

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
		return false;
	}
	
	override
	public bool ParaAvanceExplosion() {
		return false;
	}
	
	override
	public bool EsEnemigo() {
		return false;
	}
	
	override
	public bool EsObtenible() {
		return false;
	}
}
