using UnityEngine;

public class GameData : MonoBehaviour
{
    public static Vector3 playerPosition;
    public static int playerScore;

    void Start()
    {
        // ���� ����� � ������� ��� �������� �������
        Debug.Log("Player Position: " + GameData.playerPosition);
        Debug.Log("Player Score: " + GameData.playerScore);
    }
}