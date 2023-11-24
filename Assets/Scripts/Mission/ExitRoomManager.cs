using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class ExitRoomManager : MonoBehaviour
{
    public GameObject objectToRemove; // ���������� ��'���, ���� ������� ��������, � ���������
    public GameObject objectToRemove1;

    public void Start()
    {
        // ���� �� �����
        string filePath = Path.Combine(Application.persistentDataPath, "First_nightmare.txt");

        // �������� �������� �����
        if (File.Exists(filePath))
        {
            // ������� � �����
            string fileContent = File.ReadAllText(filePath);

            // �������� �������� Exit from the room
            if (fileContent.Trim() == "Exit from the room = 1;")
            {
                // ��������� ��'����, ���� Exit from the room = 1
                if (objectToRemove != null)
                {
                    Destroy(objectToRemove);
                    Destroy(objectToRemove1);
                    Debug.Log("��'��� ��������.");
                }
                else
                {
                    Debug.LogError("��'��� ��� ��������� �� �������!");
                }
            }
            else
            {
                Debug.Log("�� ��������� ��'���. Exit from the room != 1.");
            }
        }
        else
        {
            Debug.LogError("���� �� ��������!");
        }
    }
}