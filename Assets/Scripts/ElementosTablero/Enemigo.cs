// Representa un enemigo. Puede ser destruido y hace daño al jugador cuando es
// tocado por este.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public class Enemigo : ElementoTableroMovil {

	public const string EnemigoString = "Enemigo";

	// Parametros del enemigo segun el tipo
	private static float[] velocidadEnemigoPorTipo = {MovimientoEnemigo.VelocidadEnemigoDefecto, 2.5f, 4f};
	private static float[] probCambiarModoRecorridoPorTipo = {MovimientoEnemigo.ProbCambiarModoRecorridoDefecto, 0.5f, 0.3f};
	private static float[] probCambiarSentidoRecorridoPorTipo = {MovimientoEnemigo.ProbCambiarSentidoRecorridoDefecto, 0.01f, 0.2f};

	public const int CantidadTipos = 3;

	public Enemigo(GameObject elemento, int tipo):base(elemento) {
		if(tipo < CantidadTipos) {
			// Se inicializan los parametros del enemigo segun el tipo
			Elemento.GetComponent<MovimientoEnemigo>().Velocidad = velocidadEnemigoPorTipo[tipo];
			Elemento.GetComponent<MovimientoEnemigo>().ProbCambiarModoRecorrido = probCambiarModoRecorridoPorTipo[tipo];
			Elemento.GetComponent<MovimientoEnemigo>().ProbCambiarSentidoRecorrido = probCambiarSentidoRecorridoPorTipo[tipo];
		} else {
			Debug.LogError(tipo + " no es un tipo de enemigo");
		}
	}

	override
	public void Destruir() {
		GameObject.Destroy(Elemento);
	}
	
	override
	public bool EsEnemigo() {
		return true;
	}

	override
	public string ToString() {
		return EnemigoString;
	}
}