// Matriz que almacena en cada momento el contenido de cada casilla
// del tablero. Debe ser actualizado por Control.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
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
		private set {
			ancho = value;
		}
	}

	private int alto;
	public int Alto {
		get {
			return alto;
		}
		private set {
			alto = value;
		}
	}

	public Tablero(int ancho, int alto) {
		Ancho = ancho;
		Alto = alto;
		tablero = new Casilla[Alto][];
	}

	/*
	 * Metodos para gestionar el contenido del tablero
	 */
	// AddFila se usa para inicializar el tablero desde Control
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

	public Casilla GetCasilla(int i, int j) {
		return tablero[i][j];
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

	// Elimina todo el contenido de las casillas del tablero
	public void Eliminar() {
		for(int i = 0; i < Alto; i++) {
			for(int j = 0; j < Ancho; j++) {
				tablero[i][j].DestruirTodosElementos();
			}
		}
	}

	// Reserva unas casillas determinadas para que el jugador no
	// quede atrapado al principio del juego.
	public void ReservarCasillasParaJugador() {
		EliminarCasilla(1, 1);
		EliminarCasilla(1, 2);
		EliminarCasilla(1, 3);
		EliminarCasilla(2, 1);
		EliminarCasilla(3, 1);
	}

	/*
	 * Obtencion de grupos de casillas con una caracteristica comun.
	 */
	// Devuelve un ArrayList con las casillas que no tienen contenido.
	// En estas casillas se pueden instanciar enemigos.
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

	// Devuelve un ArrayList con las casillas que contienen una caja.
	// En estas casillas se pueden instanciar items.
	public ArrayList GetCasillasConCajas() {
		ArrayList casillasConCajas = new ArrayList();
		for(int i = 0; i < Alto; i++) {
			for(int j = 0; j < Ancho; j++) {
				if(tablero[i][j].HayCaja()) {
					casillasConCajas.Add(tablero[i][j]);
				}
			}
		}
		return casillasConCajas;
	}

	/**
	 * Comprobacion de tipos de elementos en una casilla
	 */
	public bool HayObstaculoEn(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayObstaculo();
	}

	public bool HayEnemigoEn(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayEnemigo();
	}

	public bool HayLlamaEn(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayLlama();
	}

	public bool HayBombaEn(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayBomba();
	}

	public bool HayElementoQuePareLlama(int i, int j) {
		Casilla casilla = GetCasilla(i, j);
		return casilla == null ? false : casilla.HayElementoQuePareLlama();
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
	
}
