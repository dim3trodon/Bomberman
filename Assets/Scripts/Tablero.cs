using UnityEngine;
using System.Collections;

public class Tablero {

	private Casilla[][] tablero;

	// Posicion para rellenar el tablero
	private int pos = 0;

	private int ancho;
	public int Ancho {
		get {
			return ancho;
		}
		set {
			ancho = value;
		}
	}

	private int alto;
	public int Alto {
		get {
			return alto;
		}
		set {
			alto = value;
		}
	}

	public Tablero(int ancho, int alto) {
		Ancho = ancho;
		Alto = alto;
		tablero = new Casilla[Alto][];
	}

	public Casilla GetCasilla(int i, int j) {
		return tablero[i][j];
	}

	public bool HayObstaculoEn(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayObstaculo();
	}

	public bool HayEnemigoEn(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayEnemigo();
	}

	public bool HayExplosionEn(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayExplosion();
	}

	public void AddFila(Casilla[] fila) {
		if(fila.Length != Ancho) {
			Debug.LogError("fila.Length debe ser " + Ancho + " pero es " + fila.Length);
		} else {
			tablero[pos] = fila;
			pos++;
		}
	}

	public Casilla SetCasilla(int i, int j, Casilla casilla) {
		tablero[i][j] = casilla;
		return casilla;
	}

	private void EliminarCasilla(int i, int j) {
		if(tablero[i][j] != null) {
			tablero[i][j].DestruirTodosElementos();
			tablero[i][j] = null;
		}
	}

	public void ReservarCasillasParaJugador() {
		EliminarCasilla(1, 1);
		EliminarCasilla(1, 2);
		EliminarCasilla(1, 3);
		EliminarCasilla(2, 1);
		EliminarCasilla(3, 1);
	}
	
}
