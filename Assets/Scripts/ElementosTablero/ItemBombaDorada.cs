using UnityEngine;
using System.Collections;

public class ItemBombaDorada : Item {

	public ItemBombaDorada(GameObject elemento):base(elemento) {}

	override
	public void Obtener() {
		Control.LlamaAtraviesaCajas = true;
		Destruir();
	}
}
