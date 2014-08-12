using UnityEngine;
using System.Collections;

public class Casilla {

	private ArrayList casilla;

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

	public Casilla() {
		casilla = new ArrayList();
	}

	public Casilla(ElementoTablero elemento) {
		casilla = new ArrayList();
		casilla.Add(elemento);
	}

	public void LimpiarExplosion() {
		ArrayList elementosQuitar = new ArrayList();
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == "Explosion") {
				elementosQuitar.Add(elemento);
			} else {
				//elemento.Elemento.transform.renderer.material = null;
			}
		}
		foreach(ElementoTablero elemento in elementosQuitar) {
			QuitarElemento(elemento);
		}
	}

	public void EliminarEnemigo() {
		int i = 0;
		while(i < casilla.Count && casilla[i].ToString() != "Enemigo") {
			i++;
		}
		if(i != casilla.Count) {
			(casilla[i] as Enemigo).Destruir();
			QuitarElemento(casilla[i] as Enemigo);
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

	public bool HayExplosion() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == "Explosion") {
				return true;
			}
		}
		return false;
	}

	public bool HayBomba() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == "Bomba") {
				return true;
			}
		}
		return false;
	}

	public bool HayElementoQuePareExplosion() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ParaAvanceExplosion()) {
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

	public void ObtenerItem() {
		int i = 0;
		while(i < casilla.Count && !(casilla[i] as ElementoTablero).EsObtenible()) {
			i++;
		}
		if(i != casilla.Count) {
			(casilla[i] as Item).Obtener();
			//(casilla[i] as ElementoTablero).Destruir();
			QuitarElemento(casilla[i] as ElementoTablero);
		}
	}

	public bool HayPuerta() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == "Puerta") {
				return true;
			}
		}
		return false;
	}

	public bool HayCaja() {
		foreach(ElementoTablero elemento in casilla) {
			if(elemento.ToString() == "Caja") {
				return true;
			}
		}
		return false;
	}
	
}
