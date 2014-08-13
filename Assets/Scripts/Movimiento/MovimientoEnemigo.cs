// Clase que mueve un enemigo.
// Un enemigo se mueve en un sentido constantemente hasta que encuentra
// un obstaculo, que lo hace cambiar de sentido. Tambien tiene una
// pequeña probabilidad de cambiar de sentido antes de encontrar
// un obstaculo.
// Tambien se mueve en dos modos: vertical y horizontalmente. Cada vez
// que se mueve hay una probabilidad de cambiar ese modo si el enemigo
// tiene suficiente espacio para cambiar de modo.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public class MovimientoEnemigo : Movimiento {

	public const int Horizontal = 0;
	public const int Vertical = 1;

	public const int SentidoA = 0;
	public const int SentidoB = 1;

	// Velocidad de enemigo por defecto
	public const float VelocidadEnemigoDefecto = 2f;

	// Probabilidades por defecto que hacen cambiar al enemigo de modo y sentido de recorrido
	public const float ProbCambiarModoRecorridoDefecto = 0.2f;
	public const float ProbCambiarSentidoRecorridoDefecto = 0.05f;

	// Indica si el enemigo se mueve en vertical o en horizontal
	private int modoRecorrido;
	public int ModoRecorrido {
		get {
			return modoRecorrido;
		}
		set {
			if(value == MovimientoEnemigo.Horizontal || value == MovimientoEnemigo.Vertical) {
				modoRecorrido = value;
			} else {
				Debug.LogError(value + " no es un modo de recorrido valido para un enemigo");
			}
		}
	}

	// Indica si el enemigo se mueve en un sentido o en otro
	private int sentidoRecorrido;
	public int SentidoRecorrido {
		get {
			return sentidoRecorrido;
		}
		set {
			if(value == MovimientoEnemigo.SentidoA || value == MovimientoEnemigo.SentidoB) {
				sentidoRecorrido = value;
			} else {
				Debug.LogError(value + " no es un sentido de recorrido valido para un enemigo");
			}
		}
	}

	// Probabilidad para que el enemigo pase de caminar horizontalmente a verticalmente
	// o viceversa.
	private float probCambiarModoRecorrido = ProbCambiarModoRecorridoDefecto;
	public float ProbCambiarModoRecorrido {
		get {
			return probCambiarModoRecorrido;
		}
		set {
			probCambiarModoRecorrido = value;
		}
	}

	// Probabilidad para que el enemigo cambie de sentido de recorrido.
	private float probCambiarSentidoRecorrido = ProbCambiarSentidoRecorridoDefecto;
	public float ProbCambiarSentidoRecorrido {
		get {
			return probCambiarSentidoRecorrido;
		}
		set {
			probCambiarSentidoRecorrido = value;
		}
	}

	private void CambiarModoRecorrido() {
		if(ModoRecorrido == Horizontal) {
			ModoRecorrido = Vertical;
		} else if(ModoRecorrido == Vertical) {
			ModoRecorrido = Horizontal;
		}
	}

	private void CambiarSentidoRecorrido() {
		if(SentidoRecorrido == SentidoA) {
			SentidoRecorrido = SentidoB;
		} else if(SentidoRecorrido == SentidoB) {
			SentidoRecorrido = SentidoA;
		}
	}

	// Comprueba si el enemigo tiene al menos una casilla a la que moverse
	// verticalmente
	private bool HayEspacioVertical() {
		return !Control.HayObstaculoEn(i - 1, j) || !Control.HayObstaculoEn(i + 1, j);
	}

	// Comprueba si el enemigo tiene al menos una casilla a la que moverse
	// horizontalmente
	private bool HayEspacioHorizontal() {
		return !Control.HayObstaculoEn(i, j - 1) || !Control.HayObstaculoEn(i, j + 1);
	}

	// Comprueba si al cambiar de modo de recorrido el enemigo se quedara atascado
	private bool SePuedeCambiarDeModoRecorrido() {
		if(ModoRecorrido == Horizontal) {
			return HayEspacioVertical();
		} else if(ModoRecorrido == Vertical) {
			return HayEspacioHorizontal();
		} else {
			return false;
		}
	}

	// Mueve al enemigo en cada frame
	void Update () {
		if(Control.HayLlamaEn(I, J)) {
			Control.EliminarEnemigoDe(I, J);
		} else if(!moviendose) {
			if(SePuedeCambiarDeModoRecorrido() 
			   && Random.value < ProbCambiarModoRecorrido) {
				CambiarModoRecorrido();
			}
			if(Random.value < ProbCambiarSentidoRecorrido) {
				CambiarSentidoRecorrido();
			}
			if(ModoRecorrido == Horizontal) {
				if(sentidoRecorrido == SentidoA) {
					jFinal = j - 1;
					horaInicio = Time.time;
				} else if(sentidoRecorrido == SentidoB) {
					jFinal = j + 1;
					horaInicio = Time.time;
				}
			} else if(ModoRecorrido == Vertical) {
				if(sentidoRecorrido == SentidoA) {
					iFinal = i - 1;
					horaInicio = Time.time;
				} else if(sentidoRecorrido == SentidoB) {
					iFinal = i + 1;
					horaInicio = Time.time;
				}
			}
			if((j != jFinal) || (i != iFinal)) {
				// Si hay un obstaculo, no moverse (iFinal y jFinal vuelve
				// a ser la posicion actual del jugador) y cambiar el 
				// sentido del recorrido
				if(Control.HayObstaculoEn(iFinal, jFinal)) {
					jFinal = j;
					iFinal = i;
					CambiarSentidoRecorrido();
				} else {
					moviendose = true;
				}
			}
		} else {
			Lerp();
		}
	}
}
