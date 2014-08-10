using UnityEngine;
using System.Collections;

public class Movimiento : MonoBehaviour {

	public const float VelocidadDefecto = 6.0f;

	public ElementoTableroMovil refElementoTableroMovil;

	protected int x;
	public int X {
		get {
			return x;
		}
		set {
			x = value;
		}
	}
	protected int xFinal;
	
	protected int z;
	public int Z {
		get {
			return z;
		}
		set {
			z = value;
		}
	}
	protected int zFinal;

	protected float velocidad = VelocidadDefecto;
	protected float Velocidad {
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

	protected int GetXTablero() {
		return Control.GetJTablero(transform.position.x);
	}

	protected int GetZTablero() {
		return Control.GetITablero(transform.position.z);
	}

	protected void MoverElemento(int x, int z) {
		if(!Control.HayObstaculoEn(x, z)) {
			xFinal = x;
			zFinal = z;
			moviendose = true;
		}
	}

	// Use this for initialization
	void Start () {
		X = xFinal = GetXTablero();
		Z = zFinal = GetZTablero();
	}

	protected void Lerp() {
		float distCovered = (Time.time - horaInicio) * velocidad;
		transform.position = Vector3.Lerp(Control.GetPosicionReal(x, z),
		                                           Control.GetPosicionReal(xFinal, zFinal),
		                                           distCovered);
		// Cuando el elemento se ha terminado de mover a la nueva posicion
		if(transform.position == Control.GetPosicionReal(xFinal, zFinal)) {
			// Actualizar posicion en Tablero
			refElementoTableroMovil.MoverA(xFinal, zFinal);
			X = xFinal;
			Z = zFinal;
			moviendose = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
