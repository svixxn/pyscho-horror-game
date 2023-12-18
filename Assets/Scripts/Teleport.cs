using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Teleport : MonoBehaviour
{
    private bool canTeleport = false;
    public TextMeshProUGUI teleportText;

    private void Awake()
    {
        teleportText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTeleport = true;
            teleportText.enabled = true;
            teleportText.text = "Press 'E' to teleport to the next level";
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
            TeleportToNextLevel();
        }
    }

    private void TeleportToNextLevel()
    {
        // Here, "NextLevelName" should be the name of the scene you want to load
        string nextLevelName = "Nightmare_1";

        SceneManager.LoadScene(nextLevelName);
    }
}