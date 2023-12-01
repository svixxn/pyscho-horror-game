using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PsycheBar : MonoBehaviour
{
    public Image barImage;
    public TextMeshProUGUI psycheText; // Додайте посилання на Text UI

    public static PsycheBar Instance { get; private set; }
    public int maxPsyche = 100;
    public int currentPsyche;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetMaxPsyche(maxPsyche);
            SetPsyche(maxPsyche);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMaxPsyche(int maxHealth)
    {
        maxPsyche = maxHealth;
        barImage.fillAmount = 1f;
        currentPsyche = maxHealth;
        UpdatePsycheText(); // Оновлення тексту після зміни значення
    }

    public void SetPsyche(int psyche)
    {
        currentPsyche = psyche;
        float normalizedPsyche = (float)psyche / maxPsyche;
        barImage.fillAmount = normalizedPsyche;
        UpdatePsycheText(); // Оновлення тексту після зміни значення
        Debug.Log("Поточне здоров'я: " + psyche);
    }

    public void TakeDamage(int damage)
    {
        currentPsyche -= damage;
        SetPsyche(currentPsyche);
        Debug.Log("Отримано урон: " + damage);
    }

    private void UpdatePsycheText()
    {
        if (psycheText != null)
        {
            psycheText.text = "Поточне здоров'я: " + currentPsyche.ToString(); // Оновлення тексту здоров'я
        }
    }
}