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

	// Matriz donde se guarda la posicion de paredes y cajas
	private static int[][] matrizParedesYCajas = new int[Ancho][];

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

	private Personaje jugador;

	// Use this for initialization
	void Start () {
		Random.seed = (int)System.DateTime.Now.Ticks;
		InicializarFilasConstantes();
		InicializarMatriz();
		RellenarTablero();
		InstanciarJugador();
	}

	private void InstanciarJugador() {
		GameObject.Instantiate(Resources.Load("Prefabs/Jugador"), 
		                       GetPosicionReal(1, 1), Quaternion.identity);
		jugador = GameObject.Find("Jugador(Clone)").GetComponent<Personaje>();
		jugador.SetPosicionInicial(1, 1);
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
		matrizParedesYCajas[0] = FilPared;
		for(int i = 1; i < Alto - 1; i++) {
			// En las filas pares poner paredes cada dos casillas
			if((i % 2 == 0)) {
				matrizParedesYCajas[i] = FilParedPar;
			}
			// En las impares, poner solo una al principio y otra
			// al final
			else {
				matrizParedesYCajas[i] = FilParedPrinFinal;
			}
			// Poner cajas aleatoriamente
			for(int j = 1; j < Ancho - 1; j++) {
				if(matrizParedesYCajas[i][j] != Pared &&  Random.value < ProbCaja) {
					matrizParedesYCajas[i][j] = Caja;
				}
			}
		}
		// Reservar ciertas casillas para que el jugador no quede atrapado
		ReservarCasillasParaJugador();

		matrizParedesYCajas[Alto - 1] = FilPared;
	}

	private void ReservarCasillasParaJugador() {
		matrizParedesYCajas[1][1] = Vacio;
		matrizParedesYCajas[1][2] = Vacio;
		matrizParedesYCajas[1][3] = Vacio;
		matrizParedesYCajas[2][1] = Vacio;
		matrizParedesYCajas[3][1] = Vacio;
	}

	public static Vector3 GetPosicionReal(int x, int z) {
		return new Vector3 (x - XBase, Y, ZBase - z);
	}

	public static bool HayObstaculo(int x, int z) {
		Debug.Log ("Hay un " + matrizParedesYCajas[x][z] + " en " + x + ", " + z);
		return matrizParedesYCajas[x][z] == Vacio ? false : true;
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
		for(int i = 0; i < matrizParedesYCajas.Length; i++) {
			int[] fila = matrizParedesYCajas[i];
			if(fila != null) {
				string imp = "";
				for(int j = 0; j < fila.Length; j++) {

					if(fila[j] == Pared) {
						InstanciarPared(j, i);
						imp += "1";
					}
					else if(fila[j] == Caja) {
						InstanciarCaja(j, i);
						imp += "C";
					} else {
						imp += "0";
					}
				}
				//Debug.Log(imp);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
