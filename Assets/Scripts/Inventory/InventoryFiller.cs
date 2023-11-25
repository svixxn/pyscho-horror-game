using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryBehaviour))]
public class InventoryFiller : MonoBehaviour
{
	[System.Serializable]
	private struct FillItem
	{
		public InventoryItemId Id;
		public int Count;
	}

	[SerializeField]
	private FillItem[] _fillItems;

	private void Start()
	{
		var inventory = GetComponent<InventoryBehaviour>();

		foreach (var item in _fillItems)
		{
			inventory.AddItem(item.Id, item.Count);
		}
	}
}
