// Representa al jugador. Puede ser destruido y no es enemigo (aunque
// esto ultimo es irrelevante).
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
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
}
