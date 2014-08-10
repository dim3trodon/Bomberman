using UnityEngine;
using System.Collections;

public abstract class ElementoTableroMovil : ElementoTablero {

	public ElementoTableroMovil(GameObject elemento):base(elemento) {
		elemento.GetComponent<Movimiento>().refElementoTableroMovil = this;
	}

	protected int GetXTablero() {
		return Elemento.GetComponent<Movimiento>().X;
	}

	protected int GetZTablero() {
		return Elemento.GetComponent<Movimiento>().Z;
	}

	public void MoverA(int x, int z) {
		Control.MoverElementoA(GetXTablero(), GetZTablero(), x, z, this);
	}

	override
	public bool EsObstaculo() {
		return false;
	}

	override
	public bool EsDestruible() {
		return true;
	}

}
