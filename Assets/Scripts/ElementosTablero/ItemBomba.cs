using UnityEngine;
using System.Collections;

public class ItemBomba : Item {

	public ItemBomba(GameObject elemento):base(elemento) {}

	override
	public void Obtener() {
		Control.AumentarBombas();
		Destruir();
	}
}
