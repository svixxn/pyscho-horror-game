using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBetweenPoints : MonoBehaviour
{
    public Transform[] waypoints; // Точки, між якими буде рухатися об'єкт
    public float moveSpeed = 3f; // Швидкість руху об'єкта
    private int currentWaypointIndex = 0; // Поточний індекс точки, до якої рухається об'єкт

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("Не вказані точки для руху об'єкта!");
            return;
        }

        // Починаємо рухатися до першої точки при старті
        transform.position = waypoints[currentWaypointIndex].position;
        StartCoroutine(MoveToNextWaypoint());
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            // Рухаємося до поточної точки
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

            // Перевіряємо, чи досягли поточної точки
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.001f) // Змінена умова на більш гнучку
            {
                // Збільшуємо індекс точки або повертаємось до першої точки, якщо досягнута остання
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }

            yield return null;
        }
    }

    // Логіка для перевірки колізії з гравцем (для 2D колайдерів)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Перевіряємо, чи це колізія з гравцем за тегом "Player"
        {
            Debug.Log("Гравець зіткнувся з об'єктом. Перезавантаження рівня...");
            SceneManager.LoadScene(2);
        }
    }
}