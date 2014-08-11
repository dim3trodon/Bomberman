using UnityEngine;
using System.Collections;

public class Jugador : ElementoTableroMovil {

	public Jugador(GameObject elemento):base(elemento) {}

	override
	public void Destruir() {
		GameObject.Destroy(Elemento);
	}

	override
	public bool EsEnemigo() {
		return false;
	}

	override
	public string ToString() {
		return "Jugador";
	}

	// Update is called once per frame
	void Update () {
		/*Debug.Log("update de jugador");
		*/
	}
}
