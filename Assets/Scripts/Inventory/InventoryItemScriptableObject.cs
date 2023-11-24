using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemSettings", menuName = "ScriptableObject/InventoryItemSettings")]
public class InventoryItemScriptableObject : ScriptableObject
{
	[SerializeField]
	private List<InventoryItem> _items;

	public InventoryItem? GetInventoryItem(InventoryItemId itemId)
	{
		foreach (var item in _items)
		{
			if (item.ItemId == itemId)
			{
				return item;
			}
		}

		return null;
	}

	private static InventoryItemScriptableObject _instance;
	public static InventoryItemScriptableObject Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Resources.Load<InventoryItemScriptableObject>("InventoryItemSettings");
			}

			return _instance;
		}
	}
}
