using UnityEngine;

public enum InventoryItemId
{
	Rake,
	Wood,
	Bucket,
    Torch
}

[System.Serializable]
public struct InventoryItem
{
	[SerializeField]
	private InventoryItemId _itemId;

	[Range(1, 1000)]
	[SerializeField]
	private int _maxInStack;

	[SerializeField]
	private Sprite _sprite;

	[SerializeField]
	private GameObject _prefab;

	public InventoryItemId ItemId { get => _itemId; }
	public int MaxInStack { get => _maxInStack; }
	public Sprite Sprite { get => _sprite; }
	public GameObject Prefab { get => _prefab; }
}
