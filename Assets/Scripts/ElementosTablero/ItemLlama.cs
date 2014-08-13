using UnityEngine;
using System.Collections;

public class ItemLlama : Item {

	public ItemLlama(GameObject elemento):base(elemento) {}

	override
	public void Obtener() {
		Control.AumentarRangoLlama();
		Destruir();
	}
}
