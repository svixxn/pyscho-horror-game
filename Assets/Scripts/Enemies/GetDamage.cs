using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float damageInterval = 1.0f;
    public float damageAmount = 10.0f;

    private PlayerController playerController;
    private float timer = 0.0f;
    public bool isEnabled = true;

    public void SetDamageEnabled(bool enableDamage)
    {
        isEnabled = enableDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            timer = damageInterval;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerController != null && isEnabled)
        {
            timer += Time.deltaTime;

            if (timer >= damageInterval)
            {
                playerController.GetDamage(damageAmount);
                timer = 0.0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = null;
        }
    }
}
