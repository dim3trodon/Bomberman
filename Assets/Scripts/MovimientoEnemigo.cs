using UnityEngine;
using System.Collections;

public class MovimientoEnemigo : Movimiento {

	public const int Horizontal = 0;
	public const int Vertical = 1;

	public const int SentidoA = 0;
	public const int SentidoB = 1;

	public const float ProbCambiarModoRecorridoDefecto = 0.2f;
	public const float ProbCambiarSentidoRecorridoDefecto = 0.2f;
	public const float SegundosEsperaDefecto = 0.1f;

	bool esperar = false;

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

	private float segundosEspera = SegundosEsperaDefecto;
	public float SegundosEspera {
		get {
			return segundosEspera;
		}
		set {
			segundosEspera = value;
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
		return !Control.HayObstaculoEn(x, z - 1) || !Control.HayObstaculoEn(x, z + 1);
	}

	// Comprueba si el enemigo tiene al menos una casilla a la que moverse
	// horizontalmente
	private bool HayEspacioHorizontal() {
		return !Control.HayObstaculoEn(x - 1, z) || !Control.HayObstaculoEn(x + 1, z);
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

	private IEnumerator Esperar() {
		esperar = true;
		Debug.Log("esperar");
		yield return new WaitForSeconds(segundosEspera);
		esperar = false;
	}

	new
	protected void Lerp() {
		base.Lerp();
		if(transform.position == Control.GetPosicionReal(xFinal, zFinal)) {
			Esperar();
		}
	}

	// Update is called once per frame
	void Update () {
		if(Control.HayExplosionEn(X, Z)) {
			Control.EliminarEnemigoDe(X, Z);
		} else if(!moviendose && !esperar) {
			if(SePuedeCambiarDeModoRecorrido() && Random.value < ProbCambiarModoRecorrido) {
				CambiarModoRecorrido();
			}
			if(Random.value < ProbCambiarSentidoRecorrido) {
				CambiarSentidoRecorrido();
			}
			if(ModoRecorrido == Horizontal) {
				if(sentidoRecorrido == SentidoA) {
					xFinal = x - 1;
					horaInicio = Time.time;
				} else if(sentidoRecorrido == SentidoB) {
					xFinal = x + 1;
					horaInicio = Time.time;
				}
			} else if(ModoRecorrido == Vertical) {
				if(sentidoRecorrido == SentidoA) {
					zFinal = z - 1;
					horaInicio = Time.time;
				} else if(sentidoRecorrido == SentidoB) {
					zFinal = z + 1;
					horaInicio = Time.time;
				}
			}
			if((x != xFinal) || (z != zFinal)) {
				// Si hay un obstaculo, no moverse (xFinal y zFinal vuelve
				// a ser la posicion actual del jugador)
				if(Control.HayObstaculoEn(xFinal, zFinal)) {
					xFinal = x;
					zFinal = z;
				} else {
					moviendose = true;
				}
			}
		} else {
			Lerp();
		}
	}
}
