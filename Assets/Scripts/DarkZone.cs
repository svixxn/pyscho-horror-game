using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkZone : MonoBehaviour
{
    public GameObject[] imagesToDisappear;
    public GameObject[] additionalObjectsToAppear;
    public Image blackoutPanel; // UI-елемент панелі затемнення

    public float darknessDuration = 2f; // Тривалість затемнення екрану
    private bool isInDarkZone = false;
    private bool hasExitedDarkZone = false;

    private void Start()
    {
        // Знаходимо панель затемнення на сцені
        blackoutPanel = GameObject.Find("BlackoutPanel").GetComponent<Image>();
        // Приховуємо панель на початку
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
        blackoutPanel.gameObject.SetActive(true); // Показуємо панель затемнення

        float timer = 0f;
        while (timer < darknessDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        blackoutPanel.gameObject.SetActive(false); // Ховаємо панель затемнення
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