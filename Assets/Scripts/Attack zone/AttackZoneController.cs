using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneController : MonoBehaviour
{
    private bool keyPressed = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            keyPressed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Killable"))
        {
            GhostController[] ghostScripts = FindObjectsOfType<GhostController>();
            foreach (GhostController ghostScript in ghostScripts)
            {
                ghostScript.DecreaseHealth(100); 
            }

            Destroy(other.gameObject); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Killable"))
        {
            keyPressed = false;
        }
    }
}