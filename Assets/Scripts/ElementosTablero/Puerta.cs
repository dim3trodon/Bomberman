﻿// Elemento del tablero que permite al jugador pasar de fase. 
// Puede ser destruido, no supone un obstaculo para los personajes ni para
// el avance de la llama. No hace daño cuando es tocado por el jugador y 
// no puede ser obtenido por este. 
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public class Puerta : ElementoTablero {

	public const string PuertaString = "Puerta";

	public Puerta(GameObject elemento):base(elemento) {}

	override
	public void Destruir() {
		GameObject.Destroy(Elemento);
	}

	override
	public bool EsObstaculo() {
		return false;
	}
	
	override
	public bool EsDestruible() {
		return false;
	}
	
	override
	public bool ParaAvanceLlama() {
		return false;
	}
	
	override
	public bool EsEnemigo() {
		return false;
	}
	
	override
	public bool EsObtenible() {
		return false;
	}

	override
	public void Obtener() {
		Debug.Log("Un " + ToString() + " no se puede obtener");
	}

	override
	public string ToString() {
		return PuertaString;
	}

}
