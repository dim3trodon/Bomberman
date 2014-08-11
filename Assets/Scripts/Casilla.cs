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

	/*public void DestruirPrimerElemento() {
		(casilla[0] as ElementoTablero).Destruir();
		casilla.RemoveAt(0);
	}*/

	public void LimpiarExplosion() {
		ArrayList elementosQuitar = new ArrayList();
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == "Explosion") {
				elementosQuitar.Add(elemento);
			} else {
				elemento.Elemento.transform.renderer.material = null;
			}
		}
		foreach(ElementoTablero elemento in elementosQuitar) {
			QuitarElemento(elemento);
		}
	}

	// Destruye Cajas
	public void DestruirCajas() {
		ArrayList elementosDestruir = new ArrayList();
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == "Caja") {
				elementosDestruir.Add(elemento);
				elemento.Destruir();
			}
		}
		foreach(ElementoTablero elemento in elementosDestruir) {
			QuitarElemento(elemento);
		}
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
		if(i >= 0) {
			casilla.Remove(elemento);
		} else {
			Debug.LogError("No se encuentra el elemento " + elemento.ToString() + " en la casilla");
		}
	}

	public bool HayObstaculo() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.EsObstaculo()) {
				return true;
			}
		}
		return false;
	}

	public bool HayObstaculoIndestructible() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.EsObstaculo() && !elemento.EsDestruible()) {
				return true;
			}
		}
		return false;
	}

	public bool HayEnemigo() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.EsEnemigo()) {
				return true;
			}
		}
		return false;
	}
	
}
