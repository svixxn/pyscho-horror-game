using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    public float viewRadius = 10f; // ����� ���� ��������
    public float darknessIntensity = 0.5f; // ������������ ������� ���� ����� ��������

    private void Update()
    {
        Collider2D[] allObjects = Physics2D.OverlapCircleAll(transform.position, viewRadius);

        List<Collider2D> allObjectsList = allObjects.ToList(); // ������������ ����� � ������

        foreach (Collider2D obj in allObjects)
        {
            // ������ �� ��'���� ��������
            obj.gameObject.SetActive(true);
        }

        // ����������� ���������� ��� ��'���� ���� ����� ��������
        Collider2D[] allObjectsInRange = Physics2D.OverlapCircleAll(transform.position, viewRadius + 1f); // ���������� ����� ��� ������� ��'���� �� ������ viewRadius

        foreach (Collider2D obj in allObjectsInRange)
        {
            if (!allObjectsList.Contains(obj))
            {
                // ������� ���� ������� �� �������
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // ����������� ������� ���� �� �������
                    spriteRenderer.color = spriteRenderer.color * darknessIntensity;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ³��������� ���� �������� ������ � �������� Unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}