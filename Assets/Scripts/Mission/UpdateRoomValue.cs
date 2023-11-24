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

        // ���������� �� �������� �������� � �������
        if (collision.CompareTag("Player"))
        {
            // ���� �� �����
            string filePath = Path.Combine(Application.persistentDataPath, "First_nightmare.txt");

            // �������� �������� �����
            if (File.Exists(filePath))
            {
                // ������ ����� � �����
                string fileContent = File.ReadAllText(filePath);

                // ���� ����� ������ "Exit from the room = "
                if (fileContent.Contains("Exit from the room = "))
                {
                    // �������� �������� Exit from the room
                    int start = fileContent.IndexOf("=") + 1;
                    int end = fileContent.IndexOf(";", start);
                    string numberStr = fileContent.Substring(start, end - start).Trim();

                    // ������������ �������� � �����
                    if (int.TryParse(numberStr, out int number))
                    {
                        // �������� �������� �� ����
                        number++;

                        // ������� ��������� �����
                        string updatedLine = "Exit from the room = " + number + ";";

                        // �������� ����� � ���� ����� ���������
                        fileContent = fileContent.Replace("Exit from the room = " + numberStr + ";", updatedLine);
                        File.WriteAllText(filePath, fileContent);

                        Debug.Log("�������� Exit from the room �������� �� ����: " + number);

                        appearancesofTentacles.Start();
                        //qd_What_dialogues_change.OnTriggerEnter2D();
                    }
                }
            }
            else
            {
                Debug.LogError("���� �� ��������!");
            }
        }
    }
}