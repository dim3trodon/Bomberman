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
}
