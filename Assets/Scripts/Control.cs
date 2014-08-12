using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	public const float Y = 0.5f;
	public const float XBase = 9.5f;
	public const float ZBase = 5.5f;
	public const int Ancho = 19;
	public const int Alto = 11;
	public const float ProbCaja = 0.33f;
	public const int IInicialJugador = 1;
	public const int JInicialJugador = 1;
	public const float DuracionExplosion = 0.9f;
	// Distancia desde el jugador a partir de la cual se 
	// instancian los enemigos aleatoriamente
	public const float DistanciaParaInstanciarEnemigos = 5f;

	// Unidad en la que aumenta la velocidad al conseguir
	// las botas
	public const float UnidadVelocidad = 5f;

	private const int NumEnemigos = 3;

	private static Control instancia;

	private static Tablero tablero;

	private static ElementoTableroMovil jugador;

	// Si llega a 0, no se pueden poner mas bombas
	private static int bolsaBombas = 2;
	public static void AumentarBombas() {
		bolsaBombas++;
	}
	public static void ReducirBombas() {
		if(bolsaBombas != 0) {
			bolsaBombas--;
		} else {
			Debug.LogError("No puede haber bombas negativas");
		}
	}
	public static bool HayBombasDisponibles() {
		return bolsaBombas > 0;
	}

	void Awake() {
		instancia = this;
	}

	// Use this for initialization
	void Start () {
		Random.seed = (int)System.DateTime.Now.Ticks;
		InicializarTablero();
		jugador = new Jugador(InstanciarJugador());
		Casilla casillaJugador = new Casilla(jugador);
		tablero.SetCasilla(IInicialJugador, JInicialJugador, casillaJugador);

		Item item = new Item("botas", InstanciarItemBotas(1, 2));
		Casilla casillaItem = tablero.GetCasilla(1, 2);
		casillaItem.AddElemento(item);
	}

	void OnGUI() {
		GUILayout.Label("Bombas: " + bolsaBombas);
		GUILayout.Label("Velocidad: " + 
		                ((jugador.Elemento.GetComponent<MovimientoJugador>().Velocidad - Movimiento.VelocidadDefecto) 
		 					/ UnidadVelocidad + 1));
	}

	public static void AumentarVelocidadPersonaje() {
		float vel = jugador.Elemento.GetComponent<MovimientoJugador>().Velocidad + UnidadVelocidad;
		jugador.Elemento.GetComponent<MovimientoJugador>().Velocidad = vel; 
	}

	private GameObject InstanciarJugador() {
		return GameObject.Instantiate(Resources.Load("Prefabs/Jugador"), 
		                       GetPosicionReal(IInicialJugador, JInicialJugador),
		                       Quaternion.identity) as GameObject;
	}

	public void InicializarTablero() {
		tablero = new Tablero(Ancho, Alto);
		Casilla[] primeraFila = new Casilla[Ancho];
		Casilla[] ultimaFila = new Casilla[Ancho];
		// Añadir primera fila
		for(int j = 0; j < Ancho; j++) {
			primeraFila[j] = new Casilla(new Bloque(InstanciarBloque(0, j)));
			ultimaFila[j] = new Casilla(new Bloque(InstanciarBloque(Alto - 1, j)));
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
						} else {
							filaIntermedia[j] = new Casilla();
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
					} else {
						filaIntermedia[j] = new Casilla();
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
		InicializarEnemigos();
	}

	private void InicializarEnemigos() {
		ArrayList casillasVacias = tablero.GetCasillasVacias();
		ArrayList casillasValidas = new ArrayList();
		foreach(Casilla casilla in casillasVacias) {
			Vector2 posJugador = new Vector2(IInicialJugador, JInicialJugador);
			// Se delimita una region en la que pueden aparecer los enemigos
			if(Vector2.Distance(new Vector2(casilla.I, casilla.J), posJugador) > DistanciaParaInstanciarEnemigos) {
				casillasValidas.Add(casilla);
			}
		}
		// Se instancian los enemigos
		for(int i = 0; i < NumEnemigos; i++) {
			Casilla casilla = casillasValidas[Random.Range(0, casillasValidas.Count)] as Casilla;
			GameObject enemigo = InstanciarEnemigo(casilla.I, casilla.J);

			enemigo.GetComponent<Movimiento>().Velocidad = 2f;
			casilla.AddElemento(new Enemigo(enemigo));
		}
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
	}

	public static void EliminarEnemigoDe(int x, int z) {
		int i = z;
		int j = x;
		Casilla casilla = tablero.GetCasilla(i, j);
		casilla.EliminarEnemigo();
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

	public static bool HayEnemigoEn(int x, int z) {
		int i = z;
		int j = x;
		return tablero.HayEnemigoEn(i, j);
	}

	public static bool HayItemEn(int x, int z) {
		int i = z;
		int j = x;
		return tablero.HayItemEn(i, j);
	}

	public static void ObtenerItemDe(int x, int z) {
		int i = z;
		int j = x;
		tablero.ObtenerItemDe(i, j);
	}

	public static bool HayExplosionEn(int x, int z) {
		int i = z;
		int j = x;
		return tablero.HayExplosionEn(i, j);
	}

	public static void PonerBomba(int x, int z) {
		int i = z;
		int j = x;
		if(HayBombasDisponibles() && !tablero.HayBombaEn(i, j)) {
			Casilla casilla = tablero.GetCasilla(i, j);
			ReducirBombas();
			if(casilla == null) {
				casilla = tablero.SetCasilla(i, j, new Casilla());
			}
			casilla.AddElemento(new Bomba(InstanciarBomba(x, z)));
		}
	}

	public static void DetonarBomba(int x, int z, ElementoTablero bomba) {
		int i = x;
		int j = z;
		tablero.GetCasilla(i, j).QuitarElemento(bomba);
		IniciarExplosion(i, j);
	}

	private static void IniciarExplosion(int i, int j) {
		//            | vUp |
		//            |.....|
		//| hIzq |....| i,j |....| hDer |
		//            |.....|
		//            |vDown|
		//
		int vUp = (i - 2) >= 0 ? i - 2 : 0;
		int vDown = (i + 3) <= Alto ? i + 3 : Alto;
		int hIzq = (j - 2) >= 0 ? j - 2 : 0;
		int hDer = (j + 3) <= Ancho ? j + 3 : Ancho;
		int v;
		int h;
		bool pararAvanceExplosion = false;

		// Casillas que contendran la explosion
		ArrayList casillas = new ArrayList();
		//Casilla casilla = tablero.GetCasilla(i, j);
		if(tablero.GetCasilla(i, j) == null) {
			tablero.SetCasilla(i, j, new Casilla());
		}
		tablero.GetCasilla(i, j).AddElemento(new Explosion(InstanciarExplosion(i, j)));
		tablero.GetCasilla(i, j).DestruirCajas();
		casillas.Add(tablero.GetCasilla(i, j));
		// Propagar hacia arriba
		v = i - 1;
		if(v >= vUp && tablero.GetCasilla(v, j) == null) {
			tablero.SetCasilla(v, j, new Casilla());
		}
		while((v >= vUp) && (!tablero.GetCasilla(v, j).HayObstaculoIndestructible()) && !pararAvanceExplosion) {
			if(tablero.GetCasilla(v, j).HayElementoQuePareExplosion()) {
				pararAvanceExplosion = true;
			}
			tablero.GetCasilla(v, j).AddElemento(new Explosion(InstanciarExplosion(v, j)));
			casillas.Add(tablero.GetCasilla(v, j));
			tablero.GetCasilla(v, j).DestruirCajas();
			v--;
			if(v >= vUp && tablero.GetCasilla(v, j) == null) {
				tablero.SetCasilla(v, j, new Casilla());
			}
		}
		// Propagar hacia abajo
		v = i + 1;
		pararAvanceExplosion = false;
		if(v < vDown && tablero.GetCasilla(v, j) == null) {
			tablero.SetCasilla(v, j, new Casilla());
		}
		while((v < vDown) && (!tablero.GetCasilla(v, j).HayObstaculoIndestructible()) && !pararAvanceExplosion) {
			if(tablero.GetCasilla(v, j).HayElementoQuePareExplosion()) {
				pararAvanceExplosion = true;
			}
			tablero.GetCasilla(v, j).AddElemento(new Explosion(InstanciarExplosion(v, j)));
			casillas.Add(tablero.GetCasilla(v, j));
			tablero.GetCasilla(v, j).DestruirCajas();
			v++;
			if(v < vDown && tablero.GetCasilla(v, j) == null) {
				tablero.SetCasilla(v, j, new Casilla());
			}
		}
		// Propagar hacia la izuierda
		h = j - 1;
		pararAvanceExplosion = false;
		if(h >= hIzq && tablero.GetCasilla(i, h) == null) {
			tablero.SetCasilla(i, h, new Casilla());
		}
		while((h >= hIzq) && (!tablero.GetCasilla(i, h).HayObstaculoIndestructible()) && !pararAvanceExplosion) {
			if(tablero.GetCasilla(i, h).HayElementoQuePareExplosion()) {
				pararAvanceExplosion = true;
			}
			tablero.GetCasilla(i, h).AddElemento(new Explosion(InstanciarExplosion(i, h)));
			casillas.Add(tablero.GetCasilla(i, h));
			tablero.GetCasilla(i, h).DestruirCajas();
			h--;
			if(h >= hIzq && tablero.GetCasilla(i, h) == null) {
				tablero.SetCasilla(i, h, new Casilla());
			}
		}
		// Propagar hacia la derecha
		h = j + 1;
		pararAvanceExplosion = false;
		if(h < hDer && tablero.GetCasilla(i, h) == null) {
			tablero.SetCasilla(i, h, new Casilla());
		}
		while((h < hDer) && (!tablero.GetCasilla(i, h).HayObstaculoIndestructible()) && !pararAvanceExplosion) {
			if(tablero.GetCasilla(i, h).HayElementoQuePareExplosion()) {
				pararAvanceExplosion = true;
			}
			tablero.GetCasilla(i, h).AddElemento(new Explosion(InstanciarExplosion(i, h)));
			casillas.Add(tablero.GetCasilla(i, h));
			tablero.GetCasilla(i, h).DestruirCajas();
			h++;
			if(h < hDer && tablero.GetCasilla(i, h) == null) {
				tablero.SetCasilla(i, h, new Casilla());
			}
		}
		StartStaticCoroutine(LimpiarExplosion(casillas));
	}

	private static IEnumerator LimpiarExplosion(ArrayList casillas) {
		yield return new WaitForSeconds(DuracionExplosion);
		foreach(Casilla casilla in casillas) {
			casilla.LimpiarExplosion();
		}
	}

	private static GameObject InstanciarExplosion(int x, int z) {
		int i = z;
		int j = x;
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Explosion"), posReal, Quaternion.identity) as GameObject;
	}

	private static GameObject InstanciarEnemigo(int x, int z) {
		int i = z;
		int j = x;
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Enemigo"), posReal, Quaternion.identity) as GameObject;
	}

	private static GameObject InstanciarItemBomba(int x, int z) {
		int i = z;
		int j = x;
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Item_bomba"), posReal, new Quaternion(0, 180, 0 ,0)) as GameObject;
	}

	private static GameObject InstanciarItemBotas(int x, int z) {
		int i = z;
		int j = x;
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Item_botas"), posReal, Quaternion.identity) as GameObject;
	}

	private static GameObject InstanciarItemLlama(int x, int z) {
		int i = z;
		int j = x;
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Item_llama"), posReal, Quaternion.identity) as GameObject;
	}

	private static GameObject InstanciarBomba(int x, int z) {
		int i = x;
		int j = z;
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Bomba"), posReal, Quaternion.identity) as GameObject;
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

	public static void FinDelJuego() {
		//GameObject.Destroy(jugador.Elemento);
	}

	public static void StartStaticCoroutine(IEnumerator rutina) {
		instancia.StartCoroutine(rutina);
	}
}
