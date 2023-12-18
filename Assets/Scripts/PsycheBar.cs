using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PsycheBar : MonoBehaviour
{
    public Image barImage;
    public TextMeshProUGUI psycheText; // ������� ��������� �� Text UI

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
        UpdatePsycheText(); // ��������� ������ ���� ���� ��������
    }

    public void SetPsyche(int psyche)
    {
        currentPsyche = psyche;
        float normalizedPsyche = (float)psyche / maxPsyche;
        barImage.fillAmount = normalizedPsyche;
        UpdatePsycheText(); // ��������� ������ ���� ���� ��������
    }

    public void TakeDamage(int damage)
    {
        currentPsyche -= damage;
        SetPsyche(currentPsyche);
    }

    private void UpdatePsycheText()
    {
        if (psycheText != null)
        {
            psycheText.text = "������� ������'�: " + currentPsyche.ToString(); // ��������� ������ ������'�
        }
    }
}