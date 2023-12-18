using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();

    public Image inventoryImage; // ������� �� ���� ��� ��������� �� UI-������� Image

    public InventorySlot[] slots; // ����� ����� ���������

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // ������������ ������� ���������� ����� Inventory
        }
        else
        {
            Destroy(gameObject); // �������� ���������� ���������� ����� Inventory
        }
        DontDestroyOnLoad(gameObject); // ���������� ��'���� Inventory ��� ����������� ����� ����
    }

    public void AddItem(Item newItem)
    {
        // ��������� ���������� ��� ������ ��������
        Sprite newSprite = newItem.icon;

        items.Add(newItem); // ������ ����� ������� �� ������ ���������
        Debug.Log("������ �������: " + newItem.name);

        // ����� ������� ���������� ����� ��� ��������� ��������
        bool inserted = false; // ��������� ��� ����������, �� ��������� �������

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.id == 0) // �������� �� ���� �� ������� � ������ �������� = 0
            {
                Debug.Log("Adding item to slot " + i);
                slots[i].AddItem(newItem); // ������ ������� � ����
                inserted = true; // ������������ ��������� ������, �� ������� ���������
                break; // �������� � �����, ���� ������� ������
            }
        }

        // ���� ������� �� ��������� ���, ������ ������� ����
        if (!inserted)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null) // �������� �������� ���������� �����
                {
                    Debug.Log("Adding item to empty slot " + i);
                    slots[i].AddItem(newItem); // ������ ������� � ������� ����
                    break; // �������� � �����, ���� ������� ������
                }
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove); // ��������� �������� � ������ ���������
            Debug.Log("�������� �������: " + itemToRemove.name);

            // �������� ���� � ���������, ���'������ �� ��������� ���������
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == itemToRemove)
                {
                    slot.ClearSlot(); // ������ ������ �������� �����
                    break;
                }
            }
        }
        else
        {
            Debug.Log("������� �� �������� � ��������: " + itemToRemove.name);
        }
    }
}


// ��������, �� ������ ���� ����� �� ���������
[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public Sprite icon; // ���������� ��� ��������
    public Sprite secondIcon; // ��������� ���������� ��� ��������
    public string description; // ���� ��������

    public Item(string itemName, int itemID, Sprite itemIcon, Sprite secondItemIcon, string itemDescription)
    {
        name = itemName;
        id = itemID;
        icon = itemIcon; // �������� ���������� �������� �� �������� ������������ Item
        secondIcon = secondItemIcon; // ��������� ���������� ��������
        description = itemDescription; // ������ ���� ��������
    }
}