using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class ExitRoomManager : MonoBehaviour
{
    public GameObject objectToRemove; // Перетягніть об'єкт, який потрібно видалити, в інспекторі
    public GameObject objectToRemove1;

    public void Start()
    {
        // Шлях до файлу
        string filePath = Path.Combine(Application.persistentDataPath, "First_nightmare.txt");

        // Перевірка наявності файлу
        if (File.Exists(filePath))
        {
            // Читання з файлу
            string fileContent = File.ReadAllText(filePath);

            // Перевірка значення Exit from the room
            if (fileContent.Trim() == "Exit from the room = 1;")
            {
                // Видалення об'єкта, якщо Exit from the room = 1
                if (objectToRemove != null)
                {
                    Destroy(objectToRemove);
                    Destroy(objectToRemove1);
                    Debug.Log("Об'єкт видалено.");
                }
                else
                {
                    Debug.LogError("Об'єкт для видалення не вказано!");
                }
            }
            else
            {
                Debug.Log("Не видаляємо об'єкт. Exit from the room != 1.");
            }
        }
        else
        {
            Debug.LogError("Файл не знайдено!");
        }
    }
}