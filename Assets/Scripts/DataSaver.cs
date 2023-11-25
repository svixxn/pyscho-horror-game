using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class DataSaver : MonoBehaviour
{
    private void Start()
    {
        // �������� ��������� ���� ����� ������� � ����
        if (CheckIfLevelIsCorrect())
        {
            // ���� ��� ���������� �����
            string filePath = Path.Combine(Application.persistentDataPath, "First_nightmare.txt");

            // ���, �� ������� ��������
            string dataToSave = "Exit from the room = 0;";

            // ����� � ��������� ����
            File.WriteAllText(filePath, dataToSave);

            Debug.Log("��� ������ �������� � ����.");
        }
    }

    // ������� ��� �������� ����
    private bool CheckIfLevelIsCorrect()
    {
        // ��� ��� �������� ���� ���
        // �������: �������� ����� ���� ��� ���� ����� ��� ���������� ��������� ����
        // ���� �������� �����, ��������� true, ������ - false

        return true; // ������ �� ���� �������� ����
    }
}
