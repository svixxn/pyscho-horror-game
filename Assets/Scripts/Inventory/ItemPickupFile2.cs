using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ItemPickupFile2 : MonoBehaviour
{
    public Item item; // Предмет, який буде підбиратися
    private Inventory inventory; // Посилання на Inventory

    private void Start()
    {
        inventory = Inventory.instance; // Отримання посилання на інстанцію Inventory
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Перевіряємо, чи була натиснута клавіша 'Е'
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Player")) // Перевіряємо чи торкається гравець
            {
                AddItemToInventory(); // Додаємо предмет до інвентаря гравця

                // Записуємо зміни в файл First_nightmare.txt
                string filePath = Path.Combine(Application.persistentDataPath, "First_nightmare.txt");

                try
                {
                    // Замінюємо весь вміст файлу на "Exit from the room = 3;"
                    File.WriteAllText(filePath, "Exit from the room = 4;");
                    Debug.Log("File content replaced with 'Exit from the room = 4;'");
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error writing to file: " + e.Message);
                }

                Destroy(gameObject); // Знищуємо об'єкт після підбору
            }
        }
    }

    void AddItemToInventory()
    {
        if (inventory != null)
        {
            inventory.AddItem(item); // Додаємо предмет до інвентаря
        }
        else
        {
            Debug.LogError("Inventory.instance is not assigned!"); // Логуємо помилку, якщо instance не ініціалізовано
        }
    }
}
