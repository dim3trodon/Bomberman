// Bomba que el jugador puede poner en el tablero y que tras unos segundos,
// detona. Es destruible, supone un obstaculo para los jugadores y los enemigos
// pero no para el avance de la llama. No hace daño al ser tocada por el jugador
// y no puede ser obtenida. Cuando la pones, explota si o si.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
using UnityEngine;
using System.Collections;

public class Bomba : ElementoTableroEstatico {

	public const float EsperaParaDetonacion = 1.5f;

	public const string BombaString = "Bomba";

	public Bomba(GameObject elemento):base(elemento) {
		Control.StartStaticCoroutine(Encender());
	}

	// Espera un tiempo determinado y detona la bomba
	private IEnumerator Encender() {
		yield return new WaitForSeconds(EsperaParaDetonacion);
		Detonar();
	}

	// Detona la bomba
	private void Detonar() {
		Control.AumentarBombas();
		Elemento.transform.renderer.enabled = false;
		Control.DetonarBomba(Control.GetITablero(Elemento.GetComponent<Transform>().position.z), 
		                     Control.GetJTablero(Elemento.GetComponent<Transform>().position.x), this);
		Destruir();
	}

	override
	public void Destruir() {
		GameObject.Destroy(Elemento);
	}

	override
	public bool EsObstaculo() {
		return true;
	}

	override
	public bool EsDestruible() {
		return true;
	}

	override
	public bool EsEnemigo() {
		return false;
	}

	override
	public bool ParaAvanceLlama() {
		return false;
	}

	override
	public bool EsObtenible() {
		return false;
	}

	override
	public string ToString() {
		return BombaString;
	}

}
