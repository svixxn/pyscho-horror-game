using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectVisible : MonoBehaviour
{
    public GameObject objectToShow; // ��'���, ���� ������� ������� �������

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objectToShow.SetActive(true); // ������ ��'��� �������, ���� ������� ��������� ����� ��������
        }
    }
}
