using UnityEngine;
using System.Collections;

public class Casilla {

	private ArrayList casilla;

	public Casilla() {
		casilla = new ArrayList();
	}

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

	public void AddElemento(ElementoTablero elemento) {
		casilla.Add(elemento);
	}

	public void QuitarElemento(ElementoTablero elemento) {
		int i = casilla.IndexOf(elemento);
		Debug.Log("Casilla se quita el elemento " + i + "(" + casilla.Count + ")");
		foreach(ElementoTablero e in casilla) {
			if(e == elemento) {
				Debug.Log("==");
			} else if(e.Equals(elemento)) {
				Debug.Log("Equals");
			} else {
				Debug.Log("no es igual");
			}
			if(e.ToString() != "Jugador") {
				Debug.LogError(e.ToString());
			}
		}
		casilla.Remove(elemento);
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
