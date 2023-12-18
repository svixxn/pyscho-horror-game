using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInteractor : MonoBehaviour
{
    public string requiredItemName; // ����� ��������, ���� ������� ������
    public TextMeshProUGUI messageText; // �����, ���� ���� ������������ ��� ���������� ��������

    private bool isInRange; // ���������, �� ������� � ��� �����䳿

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            CheckInventory(); // ���������� �������� ������ ��� �������
        }
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            UseItemFromInventory();
        }
    }

    private void UseItemFromInventory()
    {
        Inventory playerInventory = Inventory.instance;

        if (playerInventory != null && playerInventory.items.Exists(item => item.name == requiredItemName))
        {
            Item itemToRemove = playerInventory.items.Find(item => item.name == requiredItemName);

            // ϳ��� ������������ �������� - ��������� ���� � ���������
            playerInventory.RemoveItem(itemToRemove);

            // ��������� ����� ���� ������������
            messageText.text = "";

            isInRange = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ���� ������� ������ � ���������, ������� ����� � ������������ ���������, �� ������� �������� � ���� �����䳿
            messageText.text = "";
            isInRange = false;
        }
    }

    private void CheckInventory()
    {
        // �������� ��������� �� �������� ������
        Inventory playerInventory = Inventory.instance;

        // ���������� �� �������� ������ �� � ������� � �� � ����� � ���������� �������
        if (playerInventory != null && playerInventory.items.Exists(item => item.name == requiredItemName))
        {
            // ������������ ����� ��� ��������� ������������ ��������
            messageText.text = "Press 'E' to use the item: " + requiredItemName;
        }
    }
}