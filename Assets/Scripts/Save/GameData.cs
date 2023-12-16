using UnityEngine;

public class GameData : MonoBehaviour
{
    public static Vector3 playerPosition;
    public static int playerScore;

    void Start()
    {
        // Вивід даних у консоль для перевірки значень
        Debug.Log("Player Position: " + GameData.playerPosition);
        Debug.Log("Player Score: " + GameData.playerScore);
    }
}