using UnityEngine;
using System.Collections;

public class Item : ElementoTablero {

	private string tipo;
	public const string Bomba = "bomba";
	public const string Botas = "botas";
	public const string Llama = "llama";
	public const string BombaDorada = "bomba_dorada";

	public Item(string tipo, GameObject elemento):base(elemento) {
		this.tipo = tipo.ToLower();
	}

	public void Obtener() {
		Debug.Log("Obtener item "  + tipo);
		switch(tipo) {
		case (Bomba):
			Control.AumentarBombas();
			break;
		case (Botas):
			Control.AumentarVelocidadJugador();
			break;
		case(Llama):
			Control.AumentarRangoLlama();
			break;
		case(BombaDorada):
			Control.LlamaAtraviesaCajas = true;
			break;
		default:
			Debug.LogError(tipo + " no es un tipo de item");
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
