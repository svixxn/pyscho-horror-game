using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class AppearancesofTentacles : MonoBehaviour
{
    public GameObject objectToAppear; // Перетягніть об'єкт, який потрібно зробити видимим, в інспекторі
    public GameObject objectToAppear1;
    public GameObject objectToAppear2;
    public GameObject objectToAppear3;

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
            
            if (fileContent.Trim() == "Exit from the room = 2;")
            {
                // Зроблення об'єкта видимим, якщо Exit from the room = 2
                if (objectToAppear != null)
                {
                    objectToAppear.SetActive(true);
                    objectToAppear1.SetActive(true);
                    objectToAppear2.SetActive(true);
                    objectToAppear3.SetActive(true);

                    Debug.Log("Об'єкт став видимим.");
                }
                else
                {
                    Debug.LogError("Об'єкт для зроблення видимим не вказано!");
                }
            }
            else
            {
                Debug.Log("Не видаляємо об'єкт. Exit from the room не дорівнює 2.");
            }
        }
        else
        {
            Debug.LogError("Файл не знайдено!");
        }
    }
}
