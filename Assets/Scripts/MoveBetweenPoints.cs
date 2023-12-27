using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBetweenPoints : MonoBehaviour
{
    public Transform[] waypoints; // �����, �� ����� ���� �������� ��'���
    public float moveSpeed = 3f; // �������� ���� ��'����
    private int currentWaypointIndex = 0; // �������� ������ �����, �� ��� �������� ��'���

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("�� ������ ����� ��� ���� ��'����!");
            return;
        }

        // �������� �������� �� ����� ����� ��� �����
        transform.position = waypoints[currentWaypointIndex].position;
        StartCoroutine(MoveToNextWaypoint());
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            // �������� �� ������� �����
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

            // ����������, �� ������� ������� �����
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.001f) // ������ ����� �� ���� ������
            {
                // �������� ������ ����� ��� ����������� �� ����� �����, ���� ��������� �������
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }

            yield return null;
        }
    }

    // ����� ��� �������� ���糿 � ������� (��� 2D ���������)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // ����������, �� �� ����� � ������� �� ����� "Player"
        {
            Debug.Log("������� �������� � ��'�����. ���������������� ����...");
            SceneManager.LoadScene(2);
        }
    }
}