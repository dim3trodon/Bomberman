using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	public const float Y = 0.5f;
	public const float XBase = 9.5f;
	public const float ZBase = 5.5f;
	public const int Ancho = 19;
	public const int Alto = 11;

	public const float ProbCaja = 0.47f;

	private static Tablero tablero;

	private Personaje jugador;

	// Use this for initialization
	void Start () {
		Random.seed = (int)System.DateTime.Now.Ticks;
		InicializarTablero();
		InstanciarJugador();
	}

	private void InstanciarJugador() {
		GameObject.Instantiate(Resources.Load("Prefabs/Jugador"), 
		                       GetPosicionReal(1, 1), Quaternion.identity);
		jugador = GameObject.Find("Jugador(Clone)").GetComponent<Personaje>();
		jugador.SetPosicionInicial(1, 1);
	}

	public void InicializarTablero() {
		tablero = new Tablero(Ancho, Alto);
		Casilla[] primeraFila = new Casilla[Ancho];
		Casilla[] ultimaFila = new Casilla[Ancho];
		// Añadir primera fila
		for(int i = 0; i < Ancho; i++) {
			primeraFila[i] = new Casilla(new Caja(InstanciarBloque(i, 0)));
			ultimaFila[i] = new Casilla(new Caja(InstanciarBloque(i, Alto - 1)));
		}
		tablero.AddFila(primeraFila);
		// Añadir filas intermedias
		for(int i = 1; i < Alto - 1; i++) {
			Casilla[] filaIntermedia = new Casilla[Ancho];
			// Filas pares con bloques cada dos casillas
			if(i % 2 == 0) {
				for(int j = 0; j < Ancho; j++) {
					if(j % 2 == 0) {
						filaIntermedia[j] = new Casilla(new Bloque(InstanciarBloque(j, i)));
					}
					else {
						// Instanciar caja
						if(Random.value < ProbCaja) {
							filaIntermedia[j] = new Casilla(new Caja(InstanciarCaja(j, i)));
						}
					}
				}
			}
			// Filas impares con bloques solo al principio y al fial
			else {
				filaIntermedia[0] = new Casilla(new Bloque(InstanciarBloque(0, i)));
				// Instanciar cajas
				for(int j = 1; j < Ancho - 1; j++) {
					if(Random.value < ProbCaja) {
						filaIntermedia[j] = new Casilla(new Caja(InstanciarCaja(j, i)));
					}
				}
				filaIntermedia[Ancho - 1] = new Casilla (new Bloque(InstanciarBloque(Ancho - 1, i)));
			}
			tablero.AddFila(filaIntermedia);
		}
		tablero.AddFila(ultimaFila);
		// Reservar ciertas casillas para que el jugador no quede atrapado
		// al principio del juego
		tablero.ReservarCasillasParaJugador();
	}

	public static Vector3 GetPosicionReal(int x, int z) {
		return new Vector3 (x - XBase, Y, ZBase - z);
	}

	public static bool HayObstaculoEn(int x, int z) {
		return tablero.HayObstaculoEn(x, z);
	}

	private GameObject InstanciarBloque(int x, int z) {
		Vector3 posReal = GetPosicionReal(x, z);
		return GameObject.Instantiate(Resources.Load("Prefabs/Pared"), posReal, Quaternion.identity) as GameObject;
	}

	private GameObject InstanciarCaja(int x, int z) {
		Vector3 posReal = GetPosicionReal(x, z);
		return GameObject.Instantiate(Resources.Load("Prefabs/Caja"), posReal, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
