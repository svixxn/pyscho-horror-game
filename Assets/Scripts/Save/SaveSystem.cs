using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private bool savePointUsed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!savePointUsed && collision.CompareTag("Player"))
        {
            SaveGame();
            savePointUsed = true;
        }
    }

    private void SaveGame()
    {
        // Отримання всіх необхідних даних для збереження
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        int playerScore = 100; // Припустимо, це бал в грі

        // Створення об'єкту, який містить всю інформацію для збереження
        PlayerData playerData = new PlayerData(playerPosition, playerScore);

        // Збереження даних у файл
        string directoryPath = Application.persistentDataPath;
        string filePath = Path.Combine(directoryPath, "save_0.txt");

        // Збереження в форматі JSON (можна також використовувати XML або інші формати)
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, json);

        Debug.Log("Гра збережена!");
    }
}

// Припустимий клас, який містить дані гравця для збереження
[System.Serializable]
public class PlayerData
{
    public Vector3 playerPosition;
    public int playerScore;

    public PlayerData(Vector3 pos, int score)
    {
        playerPosition = pos;
        playerScore = score;
    }
}