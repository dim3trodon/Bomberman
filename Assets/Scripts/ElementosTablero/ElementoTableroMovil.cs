// Clase abstracta de la que heredan todos los elementos moviles del tablero.
// Los items moviles son atravesables, destruibles y paran el avance de la 
// llama. No pueden ser obtenidos (esto no es Pokemon).
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public abstract class ElementoTableroMovil : ElementoTablero {

	public ElementoTableroMovil(GameObject elemento):base(elemento) {
		elemento.GetComponent<Movimiento>().refElementoTableroMovil = this;
	}

	protected int GetITablero() {
		return Elemento.GetComponent<Movimiento>().I;
	}

	protected int GetJTablero() {
		return Elemento.GetComponent<Movimiento>().J;
	}

	public void MoverA(int i, int j) {
		Control.MoverElementoA(GetITablero(), GetJTablero(), i, j, this);
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
	public bool ParaAvanceLlama() {
		return true;
	}

	override
	public bool EsObtenible() {
		return false;
	}

	override
	public void Obtener() {
		Debug.LogError("Un " + ToString() + " no se puede obtener");
	}

}
