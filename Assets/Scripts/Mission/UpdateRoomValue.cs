using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;


public class UpdateRoomValue : MonoBehaviour
{
    public AppearancesofTentacles appearancesofTentacles;
    //public QuantumTek.QuantumDialogue.Demo.QD_What_dialogues_change qd_What_dialogues_change;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Перевіряємо чи колайдер зіткнувся з гравцем
        if (collision.CompareTag("Player"))
        {
            // Шлях до файлу
            string filePath = Path.Combine(Application.persistentDataPath, "First_nightmare.txt");

            // Перевірка наявності файлу
            if (File.Exists(filePath))
            {
                // Читаємо рядок з файлу
                string fileContent = File.ReadAllText(filePath);

                // Якщо рядок містить "Exit from the room = "
                if (fileContent.Contains("Exit from the room = "))
                {
                    // Отримуємо значення Exit from the room
                    int start = fileContent.IndexOf("=") + 1;
                    int end = fileContent.IndexOf(";", start);
                    string numberStr = fileContent.Substring(start, end - start).Trim();

                    // Перетворюємо значення в число
                    if (int.TryParse(numberStr, out int number))
                    {
                        // Збільшуємо значення на один
                        number++;

                        // Формуємо оновлений рядок
                        string updatedLine = "Exit from the room = " + number + ";";

                        // Замінюємо рядок в файлі новим значенням
                        fileContent = fileContent.Replace("Exit from the room = " + numberStr + ";", updatedLine);
                        File.WriteAllText(filePath, fileContent);

                        Debug.Log("Значення Exit from the room збільшено на один: " + number);

                        appearancesofTentacles.Start();
                        //qd_What_dialogues_change.OnTriggerEnter2D();
                    }
                }
            }
            else
            {
                Debug.LogError("Файл не знайдено!");
            }
        }
    }
}