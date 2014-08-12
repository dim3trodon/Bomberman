using UnityEngine;
using System.Collections;

public class Enemigo : ElementoTableroMovil {

	public Enemigo(GameObject elemento):base(elemento) {}

	public Enemigo(GameObject elemento, float velocidad):base(elemento) {
		Elemento.GetComponent<MovimientoEnemigo>().Velocidad = velocidad;
	}

	public Enemigo(GameObject elemento, float velocidad, int modoRecorrido):base(elemento) {
		Elemento.GetComponent<MovimientoEnemigo>().Velocidad = velocidad;
		Elemento.GetComponent<MovimientoEnemigo>().ModoRecorrido = modoRecorrido;
	}

	override
	public void Destruir() {
		GameObject.Destroy(Elemento);
	}
	
	override
	public bool EsEnemigo() {
		return true;
	}
}