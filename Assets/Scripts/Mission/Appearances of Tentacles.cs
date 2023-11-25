using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class AppearancesofTentacles : MonoBehaviour
{
    public GameObject objectToAppear; // ���������� ��'���, ���� ������� ������� �������, � ���������
    public GameObject objectToAppear1;
    public GameObject objectToAppear2;
    public GameObject objectToAppear3;

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
            
            if (fileContent.Trim() == "Exit from the room = 2;")
            {
                // ��������� ��'���� �������, ���� Exit from the room = 2
                if (objectToAppear != null)
                {
                    objectToAppear.SetActive(true);
                    objectToAppear1.SetActive(true);
                    objectToAppear2.SetActive(true);
                    objectToAppear3.SetActive(true);

                    Debug.Log("��'��� ���� �������.");
                }
                else
                {
                    Debug.LogError("��'��� ��� ��������� ������� �� �������!");
                }
            }
            else
            {
                Debug.Log("�� ��������� ��'���. Exit from the room �� ������� 2.");
            }
        }
        else
        {
            Debug.LogError("���� �� ��������!");
        }
    }
}
