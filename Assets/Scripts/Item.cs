using UnityEngine;
using System.Collections;

public class Item : ElementoTablero {

	private string tipo;

	public Item(string tipo, GameObject elemento):base(elemento) {
		this.tipo = tipo.ToLower();
	}

	public void Obtener() {
		Debug.Log("Obtener item "  + tipo);
		switch(tipo) {
		case ("bomba"):
			Control.AumentarBombas();
			break;
		case ("botas"):
			Control.AumentarVelocidadPersonaje();
			break;
		case("llama"):
			break;
		}
		Destruir();
	}

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
