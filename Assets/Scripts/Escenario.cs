using UnityEngine;
using System.Collections;

public class Escenario : MonoBehaviour {

	public const float Y = 0.5f;
	public const float XBase = 9.5f;
	public const float ZBase = 5.5f;
	public const int Ancho = 19;
	public const int Alto = 11;

	public const int Vacio = 0;
	public const int Pared = 1;
	public const int Caja = 2;

	public const float ProbCaja = 0.47f;

	private int[][] matrizTablero = new int[Ancho][];

	// Filas constantes
	// Una fila llena de paredes
	private int[] filPared;
	private int[] FilPared {
		get {
			return (int[]) filPared.Clone();
		}
		set {
			filPared = value;
		}
	}
	// Una fila con paredes en las posiciones pares
	private int[] filParedPar;
	private int[] FilParedPar {
		get {
			return (int[]) filParedPar.Clone();
		}
		set {
			FilParedPar = value;
		}
	}
	// Una fila con paredes al principio y al final
	private int[] filParedPrinFinal;
	private int[] FilParedPrinFinal {
		get {
			return (int[]) filParedPrinFinal.Clone();
		}
		set {
			filParedPrinFinal = value;
		}
	}

	// Use this for initialization
	void Start () {
		Random.seed = (int)System.DateTime.Now.Ticks;
		InicializarFilasConstantes();
		InicializarMatriz();
		RellenarTablero();
	}

	private void InicializarFilasConstantes() {
		// Inicializar vectores de filas constantes
		filPared = new int[Ancho];
		filParedPar = new int[Ancho];
		filParedPrinFinal = new int[Ancho];
		for(int i = 0; i < Ancho; i++) {
			filPared[i] = Pared;
			if(i % 2 == 0) {
				filParedPar[i] = Pared;
			}
		}
		filParedPrinFinal[0] = Pared;
		filParedPrinFinal[Ancho - 1] = Pared;
	}

	private void InicializarMatriz() {
		// Rellenar matriz
		matrizTablero[0] = FilPared;
		for(int i = 1; i < Alto - 1; i++) {
			// En las filas pares poner paredes cada dos casillas
			if((i % 2 == 0)) {
				matrizTablero[i] = FilParedPar;
			}
			// En las impares, poner solo una al principio y otra
			// al final
			else {
				matrizTablero[i] = FilParedPrinFinal;
			}
			// Poner cajas aleatoriamente
			for(int j = 1; j < Ancho - 1; j++) {
				if(matrizTablero[i][j] != Pared &&  Random.value < ProbCaja) {
					matrizTablero[i][j] = Caja;
				}
			}
		}
		// Reservar ciertas casillas para que el jugador no quede atrapado
		ReservarCasillasParaJugador();

		matrizTablero[Alto - 1] = FilPared;
	}

	private void ReservarCasillasParaJugador() {
		matrizTablero[1][1] = Vacio;
		matrizTablero[1][2] = Vacio;
		matrizTablero[1][3] = Vacio;
		matrizTablero[2][1] = Vacio;
		matrizTablero[3][1] = Vacio;
	}

	public Vector3 GetPosicionReal(int x, int z) {
		return new Vector3 (x - XBase, Y, ZBase - z);
	}

	private void InstanciarPared(int x, int z) {
		Vector3 posReal = GetPosicionReal(x, z);
		GameObject.Instantiate(Resources.Load("Prefabs/Pared"), posReal, Quaternion.identity);
	}

	private void InstanciarCaja(int x, int z) {
		Vector3 posReal = GetPosicionReal(x, z);
		GameObject.Instantiate(Resources.Load("Prefabs/Caja"), posReal, Quaternion.identity);
	}

	private void RellenarTablero() {
		for(int i = 0; i < matrizTablero.Length; i++) {
			int[] fila = matrizTablero[i];
			if(fila != null) {
				for(int j = 0; j < fila.Length; j++) {
					if(fila[j] == Pared) {
						InstanciarPared(j, i);
					}
					else if(fila[j] == Caja) {
						InstanciarCaja(j, i);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
