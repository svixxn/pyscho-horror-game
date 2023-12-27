using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkZone : MonoBehaviour
{
    public GameObject[] imagesToDisappear;
    public GameObject[] additionalObjectsToAppear;
    public Image blackoutPanel; // UI-������� ����� ����������

    public float darknessDuration = 2f; // ��������� ���������� ������
    private bool isInDarkZone = false;
    private bool hasExitedDarkZone = false;

    private void Start()
    {
        // ��������� ������ ���������� �� ����
        blackoutPanel = GameObject.Find("BlackoutPanel").GetComponent<Image>();
        // ��������� ������ �� �������
        blackoutPanel.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInDarkZone = true;
            StartCoroutine(DarkenScreenAndDisappear());
            ChangeAdditionalObjectsVisibility(true);
        }
    }

    private IEnumerator DarkenScreenAndDisappear()
    {
        blackoutPanel.gameObject.SetActive(true); // �������� ������ ����������

        float timer = 0f;
        while (timer < darknessDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        blackoutPanel.gameObject.SetActive(false); // ������ ������ ����������
        ChangeVisibility(false);
    }

    private void ChangeVisibility(bool visible)
    {
        foreach (GameObject image in imagesToDisappear)
        {
            image.SetActive(visible);
        }
    }

    private void ChangeAdditionalObjectsVisibility(bool visible)
    {
        foreach (GameObject obj in additionalObjectsToAppear)
        {
            obj.SetActive(visible);
        }
    }
}