using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();

    public Image inventoryImage; // Додайте це поле для посилання на UI-елемент Image

    public InventorySlot[] slots; // Масив слотів інвентаря

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Встановлення єдиного екземпляра класу Inventory
        }
        else
        {
            Destroy(gameObject); // Знищення дублюючого екземпляра класу Inventory
        }
        DontDestroyOnLoad(gameObject); // Збереження об'єкту Inventory при завантаженні нових сцен
    }

    public void AddItem(Item newItem)
    {
        // Отримання зображення для нового предмета
        Sprite newSprite = newItem.icon;

        items.Add(newItem); // Додаємо новий предмет до списку інвентаря
        Debug.Log("Додано предмет: " + newItem.name);

        // Пошук першого порожнього слота для додавання предмета
        bool inserted = false; // Прапорець для визначення, чи вставлено предмет

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.id == 0) // Перевірка чи слот не порожній і індекс предмета = 0
            {
                Debug.Log("Adding item to slot " + i);
                slots[i].AddItem(newItem); // Додаємо предмет у слот
                inserted = true; // Встановлюємо прапорець відмітки, що предмет вставлено
                break; // Виходимо з циклу, коли предмет додано
            }
        }

        // Якщо предмет не вставлено вже, шукаємо порожній слот
        if (!inserted)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null) // Перевірка наявності порожнього слоту
                {
                    Debug.Log("Adding item to empty slot " + i);
                    slots[i].AddItem(newItem); // Додаємо предмет у порожній слот
                    break; // Виходимо з циклу, коли предмет додано
                }
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove); // Видаляємо предмет зі списку інвентаря
            Debug.Log("Видалено предмет: " + itemToRemove.name);
        }
        else
        {
            Debug.Log("Предмет не знайдено в інвентарі: " + itemToRemove.name);
        }
    }
}


// Предмети, які можуть бути додані до інвентаря
[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public Sprite icon; // Зображення для предмета
    public Sprite secondIcon; // Додаткове зображення для предмета
    public string description; // Опис предмета

    public Item(string itemName, int itemID, Sprite itemIcon, Sprite secondItemIcon, string itemDescription)
    {
        name = itemName;
        id = itemID;
        icon = itemIcon; // Приймаємо зображення предмета як параметр конструктору Item
        secondIcon = secondItemIcon; // Додаткове зображення предмета
        description = itemDescription; // Додамо опис предмета
    }
}