// Clase abstracta de la que heredan todos las clases que permiten
// el movimiento de un personaje.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public abstract class Movimiento : MonoBehaviour {

	public const float VelocidadDefecto = 4.5f;

	// Referencia al elemento del tablero que mueven
	public ElementoTableroMovil refElementoTableroMovil;

	protected int j;
	public int J {
		get {
			return j;
		}
		set {
			j = value;
		}
	}
	protected int jFinal;
	
	protected int i;
	public int I {
		get {
			return i;
		}
		set {
			i = value;
		}
	}
	protected int iFinal;

	protected float velocidad = VelocidadDefecto;
	public float Velocidad {
		get {
			return velocidad;
		}
		set {
			velocidad = value;
		}
	}
	
	protected float horaInicio;
	protected float longCamino;
	
	protected bool moviendose = false;

	protected bool nuevaPosicion = false;

	protected int GetJTablero() {
		return Control.GetJTablero(transform.position.x);
	}

	protected int GetITablero() {
		return Control.GetITablero(transform.position.z);
	}

	protected void InicializarPosicion() {
		J = jFinal = GetJTablero();
		I = iFinal = GetITablero();
	}

	// Use this for initialization
	void Start () {
		InicializarPosicion();
	}

	protected void Lerp() {
		float distCovered = (Time.time - horaInicio) * Velocidad;
		transform.position = Vector3.Lerp(Control.GetPosicionReal(i, j),
		                                           Control.GetPosicionReal(iFinal, jFinal),
		                                           distCovered);
		// Cuando el elemento se ha terminado de mover a la nueva posicion
		if(transform.position == Control.GetPosicionReal(iFinal, jFinal)) {
			// Actualizar posicion en Tablero
			refElementoTableroMovil.MoverA(iFinal, jFinal);
			J = jFinal;
			I = iFinal;
			moviendose = false;
			nuevaPosicion = true;
		}
	}
}
