using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class DataSaver : MonoBehaviour
{
    private void Start()
    {
        // Перевірка потрібного рівня перед записом в файл
        if (CheckIfLevelIsCorrect())
        {
            // Шлях для збереження файлу
            string filePath = Path.Combine(Application.persistentDataPath, "First_nightmare.txt");

            // Дані, які потрібно записати
            string dataToSave = "Exit from the room = 0;";

            // Запис у текстовий файл
            File.WriteAllText(filePath, dataToSave);

            Debug.Log("Дані успішно записано у файл.");
        }
    }

    // Функція для перевірки рівня
    private bool CheckIfLevelIsCorrect()
    {
        // Ваш код перевірки рівня тут
        // Приклад: перевірка назви рівня або інша логіка для визначення потрібного рівня
        // Якщо потрібний рівень, повернути true, інакше - false

        return true; // Замініть на вашу перевірку рівня
    }
}
