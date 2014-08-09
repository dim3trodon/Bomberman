using UnityEngine;
using System.Collections;

public class Escenario : MonoBehaviour {

	public const float Y = 0.5f;
	public const float XBase = 9.5f;
	public const float ZBase = 5.5f;
	public const int ancho = 19;
	public const int alto = 11;

	public const int VACIO = 0;
	public const int PARED = 1;
	public const int CAJA = 2;

	private int[][] matrizTablero = new int[ancho][];

	// Filas constantes
	// Una fila llena de paredes
	private int[] filPared;
	// Una fila con paredes en las posiciones pares
	private int[] filParedPar;
	// Una fila con paredes al principio y al final
	private int[] filParedPrinFinal;

	// Use this for initialization
	void Start () {
		InicializarFilasConstantes();
		InicializarMatriz();
		RellenarTablero();
	}

	private void InicializarFilasConstantes() {
		// Inicializar vectores de filas constantes
		filPared = new int[ancho];
		filParedPar = new int[ancho];
		filParedPrinFinal = new int[ancho];
		for(int i = 0; i < ancho; i++) {
			filPared[i] = PARED;
			if(i % 2 == 0) {
				filParedPar[i] = PARED;
			}
		}
		filParedPrinFinal[0] = PARED;
		filParedPrinFinal[ancho - 1] = PARED;
	}

	private void InicializarMatriz() {
		// Rellenar matriz
		matrizTablero[0] = filPared;
		for(int i = 1; i < alto - 1; i++) {
			if((i % 2 == 0))
				matrizTablero[i] = filParedPar;
			else
				matrizTablero[i] = filParedPrinFinal;
		}
		matrizTablero[alto - 1] = filPared;
	}

	Vector3 GetPosicionReal(int x, int z) {
		return new Vector3 (x - XBase, Y, ZBase - z);
	}

	void InstanciarPared(int x, int z) {
		Vector3 posReal = GetPosicionReal(x, z);
		GameObject.Instantiate(Resources.Load("Prefabs/Pared"), posReal, Quaternion.identity);
	}

	void RellenarTablero() {
		// Rellenar tablero
		for(int i = 0; i < matrizTablero.Length; i++) {
			int[] fila = matrizTablero[i];
			if(fila != null) {
				for(int j = 0; j < fila.Length; j++) {
					if(fila[j] == PARED)
						InstanciarPared(j, i);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
