using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageOverDistance : MonoBehaviour
{
    public float maxDamage = 100f; // Максимальний урон
    public float damageRate = 1f; // Швидкість збільшення урону
    public float checkRadius = 1.0f; // Радіус перевірки зіткнень
    private PsycheBar psycheBar; // Посилання на скрипт полоси здоров'я

    private void Start()
    {
        // Отримати посилання на компонент PsycheBar
        psycheBar = FindObjectOfType<PsycheBar>();
        if (psycheBar == null)
        {
            Debug.LogError("PsycheBar component not found!");
        }
    }
    public void SetPsycheBar(PsycheBar bar)
    {
        psycheBar = bar;
    }
    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, checkRadius);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                float damage = maxDamage * Time.deltaTime * damageRate;
                ApplyDamage(damage);
            }
        }
    }

    public void ApplyDamage(float damage)
    {
        // Перевірка на null перед використанням
        if (psycheBar != null)
        {
            int newPsyche = Mathf.Max(psycheBar.currentPsyche - (int)damage, 0);
            psycheBar.SetPsyche(newPsyche);
            Debug.Log("Отримано урон: " + damage);
        }
        else
        {
            Debug.LogError("PsycheBar component is null!");
        }
    }
}