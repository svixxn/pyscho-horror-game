using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DoorTeleport : MonoBehaviour
{
    public Transform destination;
    private GameObject player;
    private bool canTeleport = false;
    public TextMeshProUGUI teleportText;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        teleportText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTeleport = true;
            teleportText.enabled = true;
            teleportText.text = "Press 'E' to teleport";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Hide message when player exits the trigger zone
            canTeleport = false;
            teleportText.enabled = false;
        }
    }

    private void Update()
    {
        if (canTeleport && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = destination.transform.position;
        }
    }
}
