using UnityEngine;
using System.Collections;

public abstract class Item : ElementoTablero {

	public Item(GameObject elemento):base(elemento) {}

	//public abstract void Obtener();

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
		return true;
	}
}
