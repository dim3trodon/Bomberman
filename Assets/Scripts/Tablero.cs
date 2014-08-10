﻿using UnityEngine;
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

	public Casilla GetCasilla(int x, int z) {
		return tablero[x][z];
	}

	public bool HayObstaculoEn(int x, int z) {
		Casilla casilla = GetCasilla(x, z);
		return casilla == null ? false : casilla.HayObstaculo();
	}

	public void AddFila(Casilla[] fila) {
		if(fila.Length != Ancho) {
			Debug.LogError("fila.Length debe ser " + Ancho + " pero es " + fila.Length);
		} else {
			tablero[pos] = fila;
			pos++;
		}
	}

	private void EliminarCasilla(int x, int z) {
		if(tablero[x][z] != null) {
			tablero[x][z].DestruirTodosElementos();
			tablero[x][z] = null;
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