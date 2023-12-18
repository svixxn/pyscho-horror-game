using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInteractor : MonoBehaviour
{
    public string requiredItemName; // Назва предмету, який потрібно знайти
    public TextMeshProUGUI messageText; // Текст, який буде відображатися при знаходженні предмету

    private bool isInRange; // Прапорець, чи гравець в зоні взаємодії

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            CheckInventory(); // Перевіряємо інвентар гравця при зіткненні
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

            // Після використання предмету - видаляємо його з інвентаря
            playerInventory.RemoveItem(itemToRemove);

            // Оновлюємо текст після використання
            messageText.text = "";

            isInRange = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Якщо гравець вийшов з колайдеру, очищаємо текст і встановлюємо прапорець, що гравець виходить з зони взаємодії
            messageText.text = "";
            isInRange = false;
        }
    }

    private void CheckInventory()
    {
        // Отримуємо посилання на інвентар гравця
        Inventory playerInventory = Inventory.instance;

        // Перевіряємо чи інвентар гравця не є порожнім і чи в ньому є необхідний предмет
        if (playerInventory != null && playerInventory.items.Exists(item => item.name == requiredItemName))
        {
            // Встановлюємо текст про можливість використання предмету
            messageText.text = "Press 'E' to use the item: " + requiredItemName;
        }
    }
}