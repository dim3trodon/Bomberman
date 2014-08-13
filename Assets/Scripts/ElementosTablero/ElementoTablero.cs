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

	public abstract bool ParaAvanceExplosion();

	public abstract bool EsObtenible();

	public abstract void Obtener();
}
