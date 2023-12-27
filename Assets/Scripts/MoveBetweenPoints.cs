using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBetweenPoints : MonoBehaviour
{
    [System.Serializable]
    public struct WaypointRotation
    {
        public Transform waypoint;
        public float rotationAngle;
    }

    public WaypointRotation[] waypointRotations;
    public float moveSpeed = 3f;
    private int currentWaypointIndex = 0;
    private Transform currentWaypoint;

    void Start()
    {
        if (waypointRotations.Length == 0)
        {
            Debug.LogWarning("Не вказані точки для руху об'єкта!");
            return;
        }

        currentWaypoint = waypointRotations[currentWaypointIndex].waypoint;
        transform.position = currentWaypoint.position;
        StartCoroutine(MoveToNextWaypoint());
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.001f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypointRotations.Length;
                currentWaypoint = waypointRotations[currentWaypointIndex].waypoint;
                ChangeOrientation(waypointRotations[currentWaypointIndex].rotationAngle);
            }

            yield return null;
        }
    }

    void ChangeOrientation(float rotationAngle)
    {
        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Гравець зіткнувся з об'єктом. Перезавантаження рівня...");
            SceneManager.LoadScene(2);
        }
    }
}