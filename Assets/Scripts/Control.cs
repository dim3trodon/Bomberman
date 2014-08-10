using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	public const float Y = 0.5f;
	public const float XBase = 9.5f;
	public const float ZBase = 5.5f;
	public const int Ancho = 19;
	public const int Alto = 11;
	public const float ProbCaja = 0.47f;
	public const int XInicialJugador = 1;
	public const int ZInicialJugador = 1;

	private static Tablero tablero;

	private ElementoTableroMovil jugador;

	// Use this for initialization
	void Start () {
		Random.seed = (int)System.DateTime.Now.Ticks;
		InicializarTablero();
		Casilla casillaJugador = new Casilla(new Jugador(InstanciarJugador()));
		tablero.SetCasilla(XInicialJugador, ZInicialJugador, casillaJugador);
	}

	private GameObject InstanciarJugador() {
		return GameObject.Instantiate(Resources.Load("Prefabs/Jugador"), 
		                       GetPosicionReal(XInicialJugador, ZInicialJugador),
		                       Quaternion.identity) as GameObject;
	}

	public void InicializarTablero() {
		tablero = new Tablero(Ancho, Alto);
		Casilla[] primeraFila = new Casilla[Ancho];
		Casilla[] ultimaFila = new Casilla[Ancho];
		// Añadir primera fila
		for(int j = 0; j < Ancho; j++) {
			primeraFila[j] = new Casilla(new Caja(InstanciarBloque(0, j)));
			ultimaFila[j] = new Casilla(new Caja(InstanciarBloque(Alto - 1, j)));
		}
		tablero.AddFila(primeraFila);
		// Añadir filas intermedias
		for(int i = 1; i < Alto - 1; i++) {
			Casilla[] filaIntermedia = new Casilla[Ancho];
			// Filas pares con bloques cada dos casillas
			if(i % 2 == 0) {
				for(int j = 0; j < Ancho; j++) {
					if(j % 2 == 0) {
						filaIntermedia[j] = new Casilla(new Bloque(InstanciarBloque(i, j)));
					}
					else {
						// Instanciar caja
						if(Random.value < ProbCaja) {
							filaIntermedia[j] = new Casilla(new Caja(InstanciarCaja(i, j)));
						}
					}
				}
			}
			// Filas impares con bloques solo al principio y al fial
			else {
				filaIntermedia[0] = new Casilla(new Bloque(InstanciarBloque(i, 0)));
				// Instanciar cajas
				for(int j = 1; j < Ancho - 1; j++) {
					if(Random.value < ProbCaja) {
						filaIntermedia[j] = new Casilla(new Caja(InstanciarCaja(i, j)));
					}
				}
				filaIntermedia[Ancho - 1] = new Casilla (new Bloque(InstanciarBloque(i, Ancho - 1)));
			}
			tablero.AddFila(filaIntermedia);
		}
		tablero.AddFila(ultimaFila);
		// Reservar ciertas casillas para que el jugador no quede atrapado
		// al principio del juego
		tablero.ReservarCasillasParaJugador();
	}

	public static void MoverElementoA(int xInicio, int zInicio, int xFinal, int zFinal, ElementoTableroMovil elemento) {
		int iIni = zInicio;
		int jIni = xInicio;
		int iFin = zFinal;
		int jFin = xFinal;
		Casilla casillaInicio = tablero.GetCasilla(iIni, jIni);
		Casilla casillaFinal = tablero.GetCasilla(iFin, jFin);
		if(casillaFinal == null) {
			casillaFinal = tablero.SetCasilla(iFin, jFin, new Casilla());
		}
		casillaInicio.QuitarElemento(elemento);
		casillaFinal.AddElemento(elemento);
		// TODO Evento de mover elemento
	}

	public static Vector3 GetPosicionReal(int i, int j) {
		return new Vector3 ((i - XBase), Y, (ZBase - j));
	}

	public static int GetJTablero(float xReal) {
		return (int)(xReal + XBase);
	}

	public static int GetITablero(float zReal) {
		return (int)(ZBase - zReal);
	}

	public static bool HayObstaculoEn(int x, int z) {
		int i = z;
		int j = x;
		return tablero.HayObstaculoEn(i, j);
	}

	private GameObject InstanciarBloque(int x, int z) {
		int i = z;
		int j = x;
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Pared"), posReal, Quaternion.identity) as GameObject;
	}

	private GameObject InstanciarCaja(int x, int z) {
		int i = z;
		int j = x;
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Caja"), posReal, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
