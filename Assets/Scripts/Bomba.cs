using UnityEngine;
using System.Collections;

public class Bomba : ElementoTablero {

	public const float TiempoExplosion = 1.5f;

	public Bomba(GameObject elemento):base(elemento) {
		Control.StartStaticCoroutine(Encender());
	}

	// Espera un tiempo determinado y detona la bomba
	private IEnumerator Encender() {
		yield return new WaitForSeconds(TiempoExplosion);
		Detonar();
	}

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
	public bool ParaAvanceExplosion() {
		return false;
	}

	override
	public bool EsObtenible() {
		return false;
	}

}
