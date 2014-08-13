// Item que aumenta el rango de la llama de una bomba.
// Version: 1.0
// Autor: Rodrigo Valladares Santana <rodriv_tf@hotmail.com> 
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
