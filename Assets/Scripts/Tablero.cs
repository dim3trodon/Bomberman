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

	public ArrayList GetCasillasVacias() {
		ArrayList casillasVacias = new ArrayList();
		for(int i = 0; i < Alto; i++) {
			for(int j = 0; j < Ancho; j++) {
				if(tablero[i][j].NumElementos() == 0) {
					casillasVacias.Add(tablero[i][j]);
				}
			}
		}
		return casillasVacias;
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

	public bool HayBombaEn(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayBomba();
	}

	public bool HayElementoQuePareExplosion(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayElementoQuePareExplosion();
	}

	public bool HayItemEn(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayItem();
	}

	public bool HayPuertaEn(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayPuerta();
	}

	public void ObtenerItemDe(int i, int j) {
		GetCasilla(i, j).ObtenerItem();
	}

	public void AddFila(Casilla[] fila) {
		if(fila.Length != Ancho) {
			Debug.LogError("fila.Length debe ser " + Ancho + " pero es " + fila.Length);
		} else {
			for(int j = 0; j < Ancho; j++) {
				fila[j].SetPos(pos, j);
			}
			tablero[pos] = fila;
			pos++;
		}
	}

	public Casilla SetCasilla(int i, int j, Casilla casilla) {
		tablero[i][j] = casilla;
		tablero[i][j].SetPos(i, j);
		return casilla;
	}

	private void EliminarCasilla(int i, int j) {
		if(tablero[i][j] != null) {
			tablero[i][j].DestruirTodosElementos();
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
