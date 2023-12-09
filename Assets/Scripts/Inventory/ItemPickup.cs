using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // �������, ���� ���� ����������
    private Inventory inventory; // ��������� �� Inventory

    private void Start()
    {
        inventory = Inventory.instance; // ��������� ��������� �� ��������� Inventory
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // ����������, �� ���� ��������� ������ '�'
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Player")) // ���������� �� ��������� �������
            {
                AddItemToInventory(); // ������ ������� �� ��������� ������
                Destroy(gameObject); // ������� ��'��� ���� ������
            }
        }
    }

    void AddItemToInventory()
    {
        if (inventory != null)
        {
            inventory.AddItem(item); // ������ ������� �� ���������
        }
        else
        {
            Debug.LogError("Inventory.instance is not assigned!"); // ������ �������, ���� instance �� ������������
        }
    }
}