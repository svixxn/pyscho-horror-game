using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Light playerLight; // ������ �� ����� ������

    void Start()
    {
        // �������� ����� ������ � ������� ��'����� ������
        playerLight = GetComponentInChildren<Light>();

        if (playerLight != null)
        {
            // ��������� ���������� ����� ������
            playerLight.range = 10f; // �������� �����
            playerLight.intensity = 1.5f; // ������������ �����
            playerLight.color = Color.yellow; // ���� ����� (����� ������)
        }
    }
}