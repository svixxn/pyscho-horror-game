using System.Collections;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject objectToSpawn; 
    public float spawnDelayMin = 1f; 
    public float spawnDelayMax = 5f; 
    public float minDistanceBetweenObjects = 2f;
    public LayerMask disallowedLayers; 
    public int numberOfObjectsToSpawn = 5;
    public GameObject character;

    private Coroutine spawnCoroutine;
    private Collider2D spawnAreaCollider;



    void Start()
    {
        spawnAreaCollider = GetComponent<Collider2D>();

        spawnCoroutine = StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            float delay = Random.Range(spawnDelayMin, spawnDelayMax);
            yield return new WaitForSeconds(delay);

            Vector2 randomPosition = GetRandomPositionInsideSpawnArea();

            Collider2D overlap = Physics2D.OverlapCircle(randomPosition, 0.1f, disallowedLayers);

            if (overlap == null && CheckMinimumDistance(randomPosition))
            {
                Instantiate(objectToSpawn, new Vector3(randomPosition.x, randomPosition.y, 0f), Quaternion.identity);
            }
        }
    }

    Vector2 GetRandomPositionInsideSpawnArea()
    {
        Vector2 spawnAreaSize = spawnAreaCollider.bounds.size;

        float minX = spawnAreaCollider.bounds.min.x;
        float minY = spawnAreaCollider.bounds.min.y;
        float maxX = spawnAreaCollider.bounds.max.x;
        float maxY = spawnAreaCollider.bounds.max.y;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector2(randomX, randomY);
    }

    bool CheckMinimumDistance(Vector2 position)
    {
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag(objectToSpawn.tag);

        foreach (GameObject obj in spawnedObjects)
        {
            if (Vector2.Distance(position, obj.transform.position) < minDistanceBetweenObjects)
            {
                return false;
            }
        }
        return true; 
    }

    public static void SpawnMultipleObjects(int count, GameObject character, GameObject objectToSpawn, float spawnAreaSize)
    {
        Vector2 playerPosition = character.transform.position;

        for (int i = 0; i < count; i++)
        {
            Vector2 randomPosition = GetRandomPositionNearPlayer(playerPosition, spawnAreaSize);

            Instantiate(objectToSpawn, new Vector3(randomPosition.x, randomPosition.y, 0f), Quaternion.identity);
        }
    }

    static Vector2 GetRandomPositionNearPlayer(Vector2 playerPosition, float spawnAreaSize)
    {
        float randomX = Random.Range(playerPosition.x - spawnAreaSize / 2, playerPosition.x + spawnAreaSize / 2);
        float randomY = Random.Range(playerPosition.y - spawnAreaSize / 2, playerPosition.y + spawnAreaSize / 2);

        return new Vector2(randomX, randomY);
    }
}
