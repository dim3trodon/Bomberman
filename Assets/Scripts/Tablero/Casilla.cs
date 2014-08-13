// Casilla de un tablero. Puede contener un numero indeterminado de objetos
// de la clase ElementoTablero.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
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

	/*
	 * Posicion en el tablero.
	 */
	private int i;
	public int I {
		get {
			return i;
		}
		set {
			i = value;
		}
	}
	private int j;
	public int J {
		get {
			return j;
		}
		set {
			j = value;
		}
	}
	public void SetPos(int i, int j) {
		I = i;
		J = j;
	}

	public int NumElementos() {
		return casilla.Count;
	}

	/*
	 * Eliminacion y destruccion de los elementos de la casilla
	 */
	// Elimina todos los objetos Explosion de la casilla
	public void LimpiarLlama() {
		ArrayList elementosQuitar = new ArrayList();
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == Llama.LlamaString) {
				elementosQuitar.Add(elemento);
			}
		}
		foreach(ElementoTablero elemento in elementosQuitar) {
			QuitarElemento(elemento);
		}
	}

	// Elimina todos los objetos Enemigo de la casilla
	public void EliminarEnemigo() {
		int i = 0;
		while(i < casilla.Count && casilla[i].ToString() != Enemigo.EnemigoString) {
			i++;
		}
		if(i != casilla.Count) {
			(casilla[i] as Enemigo).Destruir();
			QuitarElemento(casilla[i] as Enemigo);
		}
	}

	// Destruye todas las Cajas de la casilla
	public void DestruirCajas() {
		ArrayList elementosDestruir = new ArrayList();
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == Caja.CajaString) {
				elementosDestruir.Add(elemento);
				elemento.Destruir();
			}
		}
		foreach(ElementoTablero elemento in elementosDestruir) {
			QuitarElemento(elemento);
		}
	}

	// Destruye todos los elementos de la casilla
	public void DestruirTodosElementos() {
		foreach(ElementoTablero elemento in casilla) {
			elemento.Destruir();
		}
		casilla.Clear();
	}

	/*
	 * Metodos para añadir y quitar elementos del ArrayList de la casilla
	 */
	public void AddElemento(ElementoTablero elemento) {
		casilla.Add(elemento);
	}

	public void QuitarElemento(ElementoTablero elemento) {
		int i = casilla.IndexOf(elemento);
		if(i >= 0) {
			casilla.Remove(elemento);
		} else {
			Debug.LogError("No se encuentra el elemento " + elemento.ToString() 
			               + " en la casilla");
		}
	}

	/*
	 * Comprobar si en la casilla hay un tipo de objeto dado.
	 */
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

	public bool HayLlama() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == Llama.LlamaString) {
				return true;
			}
		}
		return false;
	}

	public bool HayBomba() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == Bomba.BombaString) {
				return true;
			}
		}
		return false;
	}

	public bool HayElementoQuePareLlama() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ParaAvanceLlama()) {
				return true;
			}
		}
		return false;
	}

	public bool HayItem() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.EsObtenible()) {
				return true;
			}
		}
		return false;
	}

	public bool HayPuerta() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == Puerta.PuertaString) {
				return true;
			}
		}
		return false;
	}

	public bool HayCaja() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == Caja.CajaString) {
				return true;
			}
		}
		return false;
	}

	// El jugador obtiene el item que se encuentra en la casilla
	public void ObtenerItem() {
		int i = 0;
		while(i < casilla.Count && !(casilla[i] as ElementoTablero).EsObtenible()) {
			i++;
		}
		if(i != casilla.Count) {
			(casilla[i] as ElementoTablero).Obtener();
			QuitarElemento(casilla[i] as ElementoTablero);
		}
	}
	
}
