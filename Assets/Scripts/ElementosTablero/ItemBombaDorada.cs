// Item que permite a la llama de una bomba atravesar cajas y enemigos.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
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
