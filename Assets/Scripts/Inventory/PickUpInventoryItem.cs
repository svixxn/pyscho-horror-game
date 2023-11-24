using UnityEngine;

public class PickUpInventoryItem : MonoBehaviour
{
    [SerializeField]
    private InventoryItemId _id;

    public InventoryItemId Id { get => _id; }
}