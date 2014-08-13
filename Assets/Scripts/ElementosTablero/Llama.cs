// Llama que aparece cuando una bomba detona.
// No es un obstaculo para el jugador y es destruible, pero mata al jugador
// cuando es tocado por esta. Una llama no para el avance de otra llama y 
// esta no puede ser obtenida por el jugador.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public class Llama : ElementoTablero {

	public const string LlamaString = "Llama";

	public Llama(GameObject elemento):base(elemento) {}

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
	public bool ParaAvanceLlama() {
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

	override
	public string ToString() {
		return LlamaString;
	}

}
