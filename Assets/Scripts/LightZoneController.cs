using UnityEngine;

public class LightZoneController : MonoBehaviour
{
    public Light lightSource; // ������ �� ������� ����� � Unity
    public LayerMask lightBlockingLayer; // ���, �� ����� �����

    void Update()
    {
        if (lightSource != null)
        {
            RaycastHit hit;
            Ray ray = new Ray(lightSource.transform.position, lightSource.transform.forward);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, lightBlockingLayer))
            {
                // ���� ����� ������������ � ��'����� � ���, �� ����� �����,
                // ��������� ��� ��'��� ��� ������� ���� ��������� (���������, ������� ������� �� ������)
                hit.transform.gameObject.SetActive(false); // ��������� ��'���

                // ���� ������� ������ ������� �� ������:
                // Renderer renderer = hit.transform.GetComponent<Renderer>();
                // renderer.material.color = Color.black;
            }
            else
            {
                // ���� ����� �� ������������ � ��'����� � ���, �����������, �� ��� ��'��� �������
                // hit.transform.gameObject.SetActive(true); // ��� ��������� ��'��� ����� �� �������� �����

                // ���� ������� ������ ������� �� �����������:
                // Renderer renderer = hit.transform.GetComponent<Renderer>();
                // renderer.material.color = Color.white;
            }
        }
    }
}