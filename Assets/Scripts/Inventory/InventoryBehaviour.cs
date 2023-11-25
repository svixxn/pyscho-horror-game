using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehaviour : MonoBehaviour
{
	public struct InventoryItemCount
	{
		private InventoryItemId _id;
		private int _count;

		public InventoryItemId Id { get => _id; }
		public int Count
		{
			get => _count;
			set
			{
				_count = value;

				var itemSettings = InventoryItemScriptableObject.Instance.GetInventoryItem(Id);
				if (itemSettings.HasValue && itemSettings.Value.MaxInStack < _count)
				{
					_count = itemSettings.Value.MaxInStack;
				}
				if (_count < 0)
				{
					_count = 0;
				}
			}
		}

		public InventoryItemCount(InventoryItemId id, int count)
		{
			_id = id;
			_count = count;
			Count = _count;
		}
	}

	private readonly List<InventoryItemCount> _items = new List<InventoryItemCount>();

	public IReadOnlyList<InventoryItemCount> Items { get => _items; }

	public void AddItem(InventoryItemId itemId, int count)
	{
		_items.Add(new InventoryItemCount(itemId, count));
	}

	public void RemoveItemAtIndex(int index)
	{
		if (_items.Count > index)
		{
			_items.RemoveAt(index);
		}
	}
}
