using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ItemPickupFile2 : MonoBehaviour
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

                // �������� ���� � ���� First_nightmare.txt
                string filePath = Path.Combine(Application.persistentDataPath, "First_nightmare.txt");

                try
                {
                    // �������� ���� ���� ����� �� "Exit from the room = 3;"
                    File.WriteAllText(filePath, "Exit from the room = 4;");
                    Debug.Log("File content replaced with 'Exit from the room = 4;'");
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error writing to file: " + e.Message);
                }

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
