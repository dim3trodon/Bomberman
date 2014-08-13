// Controla todo lo que ocurre en el tablero: instanciacion del jugador, de los enemigos, obtener
// un item, eliminacion de un item, etc.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	/*
	 * Variables y constantes relativas del tablero
	 */
	// Posicion Y de los GameObject
	public const float Y = 0.5f;
	// Punto en el que se empiezan a instanciar los GameObject en el tablero
	public const float XBase = 9.5f;
	public const float ZBase = 5.5f;
	// Ancho y alto del tablero
	public const int Ancho = 19;
	public const int Alto = 11;
	// Posicion inicial del jugador (esquina superior izquierda)
	public const int IInicialJugador = 1;
	public const int JInicialJugador = 1;
	// Duracion de la explosion cuando se pone una bomba
	public const float DuracionExplosion = 0.9f;
	// Distancia desde el jugador a partir de la cual se 
	// instancian los enemigos aleatoriamente
	public const float DistanciaParaInstanciarEnemigos = 5f;
	// Unidad en la que aumenta la velocidad del jugador al conseguir las botas
	public const float UnidadVelocidad = 5f;

	// Tablero donde se instancian los items, bloques, cajas, etc. Esta formado por objetos
	// de tipo Casilla.
	private static Tablero tablero;

	/*
	 * Variables relativas a la inicializacion del tablero
	 */
	// Numero de enemigos que se instancia aleatoriamente en la sala
	private const int NumEnemigosDefecto = 3;
	private static int numEnemigos;
	public static int NumEnemigos {
		get {
			return numEnemigos;
		}
		set {
			numEnemigos = value;
		}
	}
	// Numero de items que se colocan en el tablero (incluida la puerta)
	public const int NumItemsTablero = 5;
	// Probabilidad de que en una casilla vacia se instancie una caja
	public const float ProbCaja = 0.33f;
	// Velocidad de enemigo por defecto
	private const float VelocidadEnemigoDefecto = 2f;

	/*
	 * Variables y metodoss usados por los items
	 */
	// Numero de casillas que recorre la llama de una bomba cuando esta explota. Puede
	// ser aumentado con el item llama.
	// RangoLlamaDefecto es el rango inicial
	public const int RangoLlamaDefecto = 1;
	private static int rangoLlama;
	// Aumenta el rango de la llama
	public static void AumentarRangoLlama() {
		rangoLlama++;
	}
	// Si llamaAtraviesaCajas es false, indica si el avance de llama para cuando destruye
	// una caja o un enemigo o, por el contrario si es true, ignora los obstaculos
	// destruibles y sigue su camino normalmente. (Los bloques seguiran parando el avance 
	// de la llama). Esto lo permite el item bomba dorada.
	private static bool llamaAtraviesaCajas = false;
	public static bool LlamaAtraviesaCajas {
		get {
			return llamaAtraviesaCajas;
		}
		set {
			llamaAtraviesaCajas = value;
		}
	}
	// Indica la cantidad de bombas disponibles. Cada vez que el jugador ponga una bomba, este
	// numero se ve reducido. Vuelve a aumentar cuando la bomba detona.
	// Si llega a 0, no se pueden poner mas bombas.
	// El item bomba permite aumentar el numero de bombas maximas que se pueden poner.
	private static int bolsaBombas = 1;
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
	// Aumenta la velocidad del jugador (cuando se consigue el item de las botas)
	public static void AumentarVelocidadJugador() {
		float vel = jugador.Elemento.GetComponent<MovimientoJugador>().Velocidad + UnidadVelocidad;
		jugador.Elemento.GetComponent<MovimientoJugador>().Velocidad = vel; 
	}

	// Instancia del jugador
	private static ElementoTableroMovil jugador;

	// Instancia de Control (se inicializa en Awake) para poder llamar a corutinas desde
	// un metodo estatico. 
	private static Control instancia;

	// Se inicializar la instancia de Awake
	void Awake() {
		instancia = this;
	}

	/////////////////////////////////////////////////////////////////////////
	// Inicializacion de la fase y el tablero                              //
	/////////////////////////////////////////////////////////////////////////
	// Inicializa la semilla de Random y la fase
	void Start () {
		Random.seed = (int)System.DateTime.Now.Ticks;
		InicializarFase();
	}

	// Devuelve todos los valores a su valor por defecto e inicializa el tablero.
	private void InicializarFase() {
		numEnemigos = NumEnemigosDefecto;
		bolsaBombas = 1;
		InicializarTablero();
		jugador = new Jugador(InstanciarJugador());
		Casilla casillaJugador = new Casilla(jugador);
		tablero.SetCasilla(IInicialJugador, JInicialJugador, casillaJugador);
		rangoLlama = RangoLlamaDefecto;
		LlamaAtraviesaCajas = false;
		jugador.Elemento.GetComponent<MovimientoJugador>().Velocidad = Movimiento.VelocidadDefecto;
	}

	// Coloca los bloques y las cajas en el tablero
	public void InicializarTablero() {
		tablero = new Tablero(Ancho, Alto);
		Casilla[] primeraFila = new Casilla[Ancho];
		Casilla[] ultimaFila = new Casilla[Ancho];
		// La primera y la ultima fila del tablero esta compuesta por unicamente bloques.
		for(int j = 0; j < Ancho; j++) {
			primeraFila[j] = new Casilla(new Bloque(InstanciarBloque(0, j)));
			ultimaFila[j] = new Casilla(new Bloque(InstanciarBloque(Alto - 1, j)));
		}
		tablero.AddFila(primeraFila);
		// Añadir filas intermedias
		for(int i = 1; i < Alto - 1; i++) {
			Casilla[] filaIntermedia = new Casilla[Ancho];
			// Filas pares contienen bloques cada dos casillas
			if(i % 2 == 0) {
				for(int j = 0; j < Ancho; j++) {
					// En las columnas pares, instanciar un bloque
					if(j % 2 == 0) {
						filaIntermedia[j] = new Casilla(new Bloque(InstanciarBloque(i, j)));
					}
					// En las filas impares, instanciar una caja o no instanciar nada
					else {
						if(Random.value < ProbCaja) {
							filaIntermedia[j] = new Casilla(new Caja(InstanciarCaja(i, j)));
						} else {
							filaIntermedia[j] = new Casilla();
						}
					}
				}
			}
			// Filas impares contienen bloques solo al principio y al fial
			else {
				// Primera columna de la fila
				filaIntermedia[0] = new Casilla(new Bloque(InstanciarBloque(i, 0)));
				// Instanciar cajas
				for(int j = 1; j < Ancho - 1; j++) {
					if(Random.value < ProbCaja) {
						filaIntermedia[j] = new Casilla(new Caja(InstanciarCaja(i, j)));
					} else {
						filaIntermedia[j] = new Casilla();
					}
				}
				// Ultima columna de la fila
				filaIntermedia[Ancho - 1] = new Casilla (new Bloque(InstanciarBloque(i, Ancho - 1)));
			}
			tablero.AddFila(filaIntermedia);
		}
		tablero.AddFila(ultimaFila);
		// Reservar ciertas casillas para que el jugador no quede atrapado
		// al principio del juego
		tablero.ReservarCasillasParaJugador();
		InicializarEnemigos();
		InicializarItemsYPuerta();
	}

	// Instancia tres enemigos aleatoriamente en las casillas vacias
	private void InicializarEnemigos() {
		ArrayList casillasVacias = tablero.GetCasillasVacias();
		// Contiene las casillas en las que se puede instanciar un enemigo
		ArrayList casillasValidas = new ArrayList();
		foreach(Casilla casilla in casillasVacias) {
			Vector2 posJugador = new Vector2(IInicialJugador, JInicialJugador);
			// Se delimita una region en la que pueden aparecer enemigos al principio del juego
			if(Vector2.Distance(new Vector2(casilla.I, casilla.J), posJugador) 
			   	> DistanciaParaInstanciarEnemigos) {
				casillasValidas.Add(casilla);
			}
		}
		// Se instancian los enemigos
		for(int i = 0; i < NumEnemigos; i++) {
			Casilla casilla = casillasValidas[Random.Range(0, casillasValidas.Count)] as Casilla;
			GameObject enemigo = InstanciarEnemigo(casilla.I, casilla.J);
			enemigo.GetComponent<Movimiento>().Velocidad = VelocidadEnemigoDefecto;
			casilla.AddElemento(new Enemigo(enemigo));
			casillasValidas.Remove(casilla);
		}
	}

	// Inicializa los items obtenibles por el jugador y la puerta para pasar de fase.
	private void InicializarItemsYPuerta() {
		// Los items y la puerta solo aparecen debajo de cajas.
		ArrayList casillasConCajas = tablero.GetCasillasConCajas();
		// Añadir puerta
		int pos = Random.Range(0, casillasConCajas.Count);
		int i = (casillasConCajas[pos] as Casilla).I;
		int j = (casillasConCajas[pos] as Casilla).J;
		(casillasConCajas[pos] as Casilla).AddElemento(new Puerta(InstanciarPuerta(i, j)));
		casillasConCajas.RemoveAt(pos);
		// Añadir item bomba
		pos = Random.Range(0, casillasConCajas.Count);
		i = (casillasConCajas[pos] as Casilla).I;
		j = (casillasConCajas[pos] as Casilla).J;
		(casillasConCajas[pos] as Casilla).AddElemento(new ItemBomba(InstanciarItemBomba(i, j)));
		casillasConCajas.RemoveAt(pos);
		// Añadir item botas
		pos = Random.Range(0, casillasConCajas.Count);
		i = (casillasConCajas[pos] as Casilla).I;
		j = (casillasConCajas[pos] as Casilla).J;
		(casillasConCajas[pos] as Casilla).AddElemento(new ItemBotas(InstanciarItemBotas(i, j)));
		casillasConCajas.RemoveAt(pos);
		// Añadir item llama
		pos = Random.Range(0, casillasConCajas.Count);
		i = (casillasConCajas[pos] as Casilla).I;
		j = (casillasConCajas[pos] as Casilla).J;
		(casillasConCajas[pos] as Casilla).AddElemento(new ItemLlama(InstanciarItemLlama(i, j)));
		casillasConCajas.RemoveAt(pos);
		// Añadir item bomba dorada
		pos = Random.Range(0, casillasConCajas.Count);
		i = (casillasConCajas[pos] as Casilla).I;
		j = (casillasConCajas[pos] as Casilla).J;
		(casillasConCajas[pos] as Casilla).AddElemento(new ItemBombaDorada(InstanciarItemBombaDorada(i, j)));
		casillasConCajas.RemoveAt(pos);
	}

	// Elimina todos los elementos del tablero excepto los bloques.
	private void EliminarTablero() {
		tablero.Eliminar();
	}
	
	// Indica si el jugador ha eliminado todos los enemigos y puede pasar de fase.
	public static bool SePuedePasarDeFase() {
		return numEnemigos == 0;
	}
	
	// TODO Pasa a la siguiente fase
	public static void SiguienteFase() {
		if(SePuedePasarDeFase()) {
			Debug.Log("Siguiente fase");
		} else {
			Debug.LogError("Aun no se puede pasar de fase. Enemigos: " + numEnemigos);
		}
	}

	/////////////////////////////////////////////////////////////////////////
	// Mover un elemento del tablero y posiciones en el tablero            //
	/////////////////////////////////////////////////////////////////////////

	// A partir de una posicion en la matriz del tablero, se obtiene la posicion
	// real en el mundo 3D.
	public static Vector3 GetPosicionReal(int i, int j) {
		return new Vector3 ((j - XBase), Y, (ZBase - i));
	}
	
	// A partir de una posicion x real en el mundo 3D, se obtiene la posicion j
	// del tablero
	public static int GetJTablero(float xReal) {
		return (int)(xReal + XBase);
	}
	
	// A partir de una posicion z real en el mundo 3D, se obtiene la posicion i
	// del tablero
	public static int GetITablero(float zReal) {
		return (int)(ZBase - zReal);
	}

	// Mueve un elemento de una posicion inicial del tablero a una final. 
	public static void MoverElementoA(int iIni, int jIni, int iFin, int jFin, 
	                                  ElementoTableroMovil elemento) {
		Casilla casillaInicio = tablero.GetCasilla(iIni, jIni);
		Casilla casillaFinal = tablero.GetCasilla(iFin, jFin);
		if(casillaFinal == null) {
			casillaFinal = tablero.SetCasilla(iFin, jFin, new Casilla());
		}
		casillaInicio.QuitarElemento(elemento);
		casillaFinal.AddElemento(elemento);
	}

	// Elimina un enemigo del tablero
	public static void EliminarEnemigoDe(int i, int j) {
		Casilla casilla = tablero.GetCasilla(i, j);
		casilla.EliminarEnemigo();
		NumEnemigos--;
	}

	/////////////////////////////////////////////////////////////////////////
	// Comprobacion de un tipo de elemento del tablero en una posicion     //
	/////////////////////////////////////////////////////////////////////////

	// Comprueba si hay un obstaculo en la posicion i, j del tablero
	public static bool HayObstaculoEn(int i, int j) {
		return tablero.HayObstaculoEn(i, j);
	}
	
	// Comprueba si hay un enemigo en la posicion i, j del tablero
	public static bool HayEnemigoEn(int i, int j) {
		return tablero.HayEnemigoEn(i, j);
	}

	// Comprueba si hay un item en la posicion i, j del tablero
	public static bool HayItemEn(int i, int j) {
		return tablero.HayItemEn(i, j);
	}

	// Comprueba si hay una llama en la posicion i, j del tablero
	public static bool HayLlamaEn(int i, int j) {
		return tablero.HayLlamaEn(i, j);
	}
	
	// Comprueba si hay una puerta en la posicion i, j del tablero
	public static bool HayPuertaEn(int i, int j) {
		return tablero.HayPuertaEn(i, j);
	}

	// Se obtiene el item que se encuentra en la posicion i, j del tablero
	public static void ObtenerItemDe(int i, int j) {
		tablero.ObtenerItemDe(i, j);
	}

	/////////////////////////////////////////////////////////////////////////
	// Metodos relativos a la bomba y su detonacion                        //
	/////////////////////////////////////////////////////////////////////////

	// Se instancia una bomba en la posicion i, j del tablero
	public static void PonerBomba(int i, int j) {
		if(HayBombasDisponibles() && !tablero.HayBombaEn(i, j)) {
			Casilla casilla = tablero.GetCasilla(i, j);
			ReducirBombas();
			if(casilla == null) {
				casilla = tablero.SetCasilla(i, j, new Casilla());
			}
			casilla.AddElemento(new Bomba(InstanciarBomba(i, j)));
		}
	}

	// Se detona la bomba de la posicion i, j del tablero
	public static void DetonarBomba(int i, int j, ElementoTablero bomba) {
		tablero.GetCasilla(i, j).QuitarElemento(bomba);
		IniciarExplosion(i, j);
	}

	// Inicia una explosion en la posicion i, j del tablero. El avance de la explosion se
	// para cuando esta se encuentra un obstaculo (caja o bloque) o un enemigo. Si el 
	// jugador ha obtenido el item bomba dorada, la explosion no se para cuando se encuentra
	// una caja o un enemigo.
	private static void IniciarExplosion(int i, int j) {
		//            | vUp |
		//            |.....|
		//| hIzq |....| i,j |....| hDer |
		//            |.....|
		//            |vDown|
		//
		// Se limita el rango de la explosion en caso de que se salga de los 
		// limites del tablero
		int vUp = (i - rangoLlama) >= 0 ? i - rangoLlama : 0;
		int vDown = (i + rangoLlama + 1) <= Alto ? i + rangoLlama + 1 : Alto;
		int hIzq = (j - rangoLlama) >= 0 ? j - rangoLlama : 0;
		int hDer = (j + rangoLlama + 1) <= Ancho ? j + rangoLlama + 1 : Ancho;
		int v;
		int h;
		// Indica si se debe parar el avance de la explosion (por un obstaculo)
		bool pararAvanceExplosion = false;
		// Casillas que contendran la explosion
		ArrayList casillas = new ArrayList();
		// Explosion en la posicion i, j del tablero (posicion inicial)
		if(tablero.GetCasilla(i, j) == null) {
			tablero.SetCasilla(i, j, new Casilla());
		}
		tablero.GetCasilla(i, j).AddElemento(new Llama(InstanciarExplosion(i, j)));
		tablero.GetCasilla(i, j).DestruirCajas();
		casillas.Add(tablero.GetCasilla(i, j));
		// Propagar hacia arriba
		v = i - 1;
		while((v >= vUp) && (!tablero.GetCasilla(v, j).HayObstaculoIndestructible()) && !pararAvanceExplosion) {
			if(!LlamaAtraviesaCajas && tablero.GetCasilla(v, j).HayElementoQuePareLlama()) {
				pararAvanceExplosion = true;
			}
			tablero.GetCasilla(v, j).AddElemento(new Llama(InstanciarExplosion(v, j)));
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
		while((v < vDown) && (!tablero.GetCasilla(v, j).HayObstaculoIndestructible()) && !pararAvanceExplosion) {
			if(!LlamaAtraviesaCajas && tablero.GetCasilla(v, j).HayElementoQuePareLlama()) {
				pararAvanceExplosion = true;
			}
			tablero.GetCasilla(v, j).AddElemento(new Llama(InstanciarExplosion(v, j)));
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
		while((h >= hIzq) && (!tablero.GetCasilla(i, h).HayObstaculoIndestructible()) && !pararAvanceExplosion) {
			if(!LlamaAtraviesaCajas && tablero.GetCasilla(i, h).HayElementoQuePareLlama()) {
				pararAvanceExplosion = true;
			}
			tablero.GetCasilla(i, h).AddElemento(new Llama(InstanciarExplosion(i, h)));
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
		while((h < hDer) && (!tablero.GetCasilla(i, h).HayObstaculoIndestructible()) && !pararAvanceExplosion) {
			if(!LlamaAtraviesaCajas && tablero.GetCasilla(i, h).HayElementoQuePareLlama()) {
				pararAvanceExplosion = true;
			}
			tablero.GetCasilla(i, h).AddElemento(new Llama(InstanciarExplosion(i, h)));
			casillas.Add(tablero.GetCasilla(i, h));
			tablero.GetCasilla(i, h).DestruirCajas();
			h++;
			if(h < hDer && tablero.GetCasilla(i, h) == null) {
				tablero.SetCasilla(i, h, new Casilla());
			}
		}
		StartStaticCoroutine(LimpiarLlama(casillas));
	}

	// Elimina la llama que deja la explosion
	private static IEnumerator LimpiarLlama(ArrayList casillas) {
		yield return new WaitForSeconds(DuracionExplosion);
		foreach(Casilla casilla in casillas) {
			casilla.LimpiarLlama();
		}
	}

	/////////////////////////////////////////////////////////////////////////
	// Instanciacion de elementos del tablero                              //
	/////////////////////////////////////////////////////////////////////////

	// Instancia el jugador en la posicion por defecto
	private GameObject InstanciarJugador() {
		return GameObject.Instantiate(Resources.Load("Prefabs/Jugador"), 
		                              GetPosicionReal(IInicialJugador, JInicialJugador),
		                              Quaternion.identity) as GameObject;
	}

	// Instancia un bloque en la posicion i, j
	private GameObject InstanciarBloque(int i, int j) {
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Pared"), posReal, Quaternion.identity) as GameObject;
	}
	
	// Instancia una caja en la posicion i, j
	private GameObject InstanciarCaja(int i, int j) {
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Caja"), posReal, Quaternion.identity) as GameObject;
	}

	// Instancia un enemigo en la posicion i, j
	private static GameObject InstanciarEnemigo(int i, int j) {
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Enemigo"), posReal, Quaternion.identity) as GameObject;
	}

	// Instancia una bomba en la posicion i, j
	private static GameObject InstanciarBomba(int i, int j) {
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Bomba"), posReal, Quaternion.identity) as GameObject;
	}

	// Instancia una explosion en la posicion i, j
	private static GameObject InstanciarExplosion(int i, int j) {
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Explosion"), posReal, Quaternion.identity) as GameObject;
	}

	// Instancia un item bomba en la posicion i, j
	private static GameObject InstanciarItemBomba(int i, int j) {
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Item_bomba"), posReal, new Quaternion(0, 180, 0 ,0)) as GameObject;
	}

	// Instanciacion de items                                           //
	// Instancia un item botas en la posicion i, j
	private static GameObject InstanciarItemBotas(int i, int j) {
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Item_botas"), posReal, new Quaternion(0, 180, 0 ,0)) as GameObject;
	}

	// Instancia un item llama en la posicion i, j
	private static GameObject InstanciarItemLlama(int i, int j) {
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Item_llama"), posReal, new Quaternion(0, 180, 0 ,0)) as GameObject;
	}

	// Instancia un item bomba dorada en la posicion i, j
	private static GameObject InstanciarItemBombaDorada(int i, int j) {
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Item_bomba_dorada"), posReal, new Quaternion(0, 180, 0 ,0)) as GameObject;
	}

	// Instancia una puerta en la posicion i, j
	private GameObject InstanciarPuerta(int i, int j) {
		Vector3 posReal = GetPosicionReal(i, j);
		return GameObject.Instantiate(Resources.Load("Prefabs/Puerta"), posReal, Quaternion.identity) as GameObject;
	}

	/////////////////////////////////////////////////////////////////////////
	// Otros metodos						                               //
	/////////////////////////////////////////////////////////////////////////

	// Metodo que se llama cuando el jugador pierde
	public static void FinDelJuego() {
		Debug.Log("Fin del juego");
		/*StartStaticCoroutine(MostrarPantallaNegra(0.5f));
		instancia.EliminarTablero();
		instancia.InicializarFase();*/
	}

	// Corutina que espera unos segundos determinados
	private static IEnumerator Esperar(float segundos) {
		yield return new WaitForSeconds(segundos);
	}
	
	// Muestra una pantalla en negro (util para cambios entre escena)
	private static IEnumerator MostrarPantallaNegra(float segundos) {
		int cullingMask = Camera.main.cullingMask;
		Camera.main.cullingMask = 0;
		yield return StartStaticCoroutine(Esperar(segundos));
		Camera.main.cullingMask = cullingMask;
	}

	// Metodo que inicia una corutina. Sirve para llamar corutinas desde metodos estaticos.
	public static Coroutine StartStaticCoroutine(IEnumerator rutina) {
		return instancia.StartCoroutine(rutina);
	}

	// Muestra informacion de los items del personaje y los enemigos que quedan en la fase
	void OnGUI() {
		GUILayout.Label("Enemigos: " + NumEnemigos);
		GUILayout.Label("Bombas: " + bolsaBombas);
		GUILayout.Label("Velocidad: " + 
		                ((jugador.Elemento.GetComponent<MovimientoJugador>().Velocidad 
		  - Movimiento.VelocidadDefecto) / UnidadVelocidad + 1));
		GUILayout.Label("Llama: " + rangoLlama);
		if(LlamaAtraviesaCajas) {
			GUILayout.Label("Bomba dorada");
		}
	}
}
