using UnityEngine;
using System.Collections;

public class Personaje : MonoBehaviour {

	public const float VelocidadDefecto = 6.0f;

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

	protected void Lerp() {
		float distCovered = (Time.time - horaInicio) * velocidad;
		transform.position = Vector3.Lerp(Control.GetPosicionReal(x, z),
		                                  Control.GetPosicionReal(xFinal, zFinal),
		                                  distCovered);
		if(transform.position == Control.GetPosicionReal(xFinal, zFinal)) {
			x = xFinal;
			z = zFinal;
			moviendose = false;
		}
	}

	public void SetPosicionInicial(int x, int z) {
		X = x;
		Z = z;
		xFinal = x;
		zFinal = z;
	}
}
