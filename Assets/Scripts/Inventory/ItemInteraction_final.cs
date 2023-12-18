using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class ItemInteractor_final : MonoBehaviour
{
    public string requiredItemName; // Назва предмету, який потрібно знайти
    public TextMeshProUGUI messageText; // Текст, який буде відображатися при знаходженні предмету

    private bool isInRange; // Прапорець, чи гравець в зоні взаємодії

    public Tilemap visibleTilemap; // Посилання на тайлмап, який має стати видимим
    public Tilemap visibleTilemap_2; // Посилання на тайлмап, який має стати видимим


    public GameObject objectToRemove; // Посилання на об'єкт, який має бути видалений

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

            // Зробити тайлмап видимим, якщо він був призначений через інспектор
            if (visibleTilemap != null)
            {
                visibleTilemap.gameObject.SetActive(true);
                visibleTilemap_2.gameObject.SetActive(true);
            }

            // Видалення одного об'єкта
            Destroy(objectToRemove); // Знищення об'єкта, який потрібно видалити
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