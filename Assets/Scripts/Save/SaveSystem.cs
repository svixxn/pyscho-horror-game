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
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        int playerScore = 100;
        
        PlayerData playerData = new PlayerData(playerPosition, playerScore);

        string directoryPath = Application.persistentDataPath;
        string filePath = Path.Combine(directoryPath, "save_0.txt");

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, json);
    }
}

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