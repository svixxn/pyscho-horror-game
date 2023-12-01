using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageOverDistance : MonoBehaviour
{
    public float maxDamage = 100f; // ������������ ����
    public float damageRate = 1f; // �������� ��������� �����
    public float checkRadius = 1.0f; // ����� �������� �������
    private PsycheBar psycheBar; // ��������� �� ������ ������ ������'�

    private void Start()
    {
        // �������� ��������� �� ��������� PsycheBar
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
        // �������� �� null ����� �������������
        if (psycheBar != null)
        {
            int newPsyche = Mathf.Max(psycheBar.currentPsyche - (int)damage, 0);
            psycheBar.SetPsyche(newPsyche);
            Debug.Log("�������� ����: " + damage);
        }
        else
        {
            Debug.LogError("PsycheBar component is null!");
        }
    }
}