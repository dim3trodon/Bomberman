using UnityEngine;
using System.Collections;

public class Casilla {

	private ArrayList casilla;

	public Casilla(ElementoTablero elemento) {
		casilla = new ArrayList();
		casilla.Add(elemento);
	}

	public void DestruirPrimerElemento() {
		(casilla[0] as ElementoTablero).Destruir();
		casilla.RemoveAt(0);
	}

	public void DestruirTodosElementos() {
		foreach(ElementoTablero elemento in casilla) {
			elemento.Destruir();
		}
		casilla.Clear();
	}

	public bool HayObstaculo() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.EsObstaculo()) {
				return true;
			}
		}
		return false;
	}
	
}
