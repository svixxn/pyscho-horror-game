using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchZone : MonoBehaviour
{

    public GameObject character;

    public float spawnAreaSize = 15f;
    public GameObject objectToSpawn;
    public int spawnCount = 5;

    private bool hasEntered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasEntered && other.CompareTag("Player"))
        {
            hasEntered = true;
            SpawnObjects.SpawnMultipleObjects(spawnCount, character, objectToSpawn, spawnAreaSize);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (hasEntered && other.CompareTag("Player"))
        {
            hasEntered = false;
        }
    }

}
