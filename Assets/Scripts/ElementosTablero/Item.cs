// Clase abstracta de la que heredan todos los items.
// Los items no suponen un obstaculo para el jugador ni el avance de la llama
// de la bomba. Se pueden destruir, no hacen daño al ser tocados por el
// jugador y pueden ser obtenidos.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public abstract class Item : ElementoTablero {

	public Item(GameObject elemento):base(elemento) {}

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
	public bool ParaAvanceLlama() {
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
