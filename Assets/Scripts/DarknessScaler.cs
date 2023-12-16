using UnityEngine;

public class DarknessScaler : MonoBehaviour
{
    private int maxHealth = 100; // Maximum health
    private int currentHealth; // Current health

    public float minXSize = 18f; // Minimum size on X-axis when HP is low
    public float maxXSize = 1.5f; // Maximum size on X-axis when HP is full
    public float minYSize = 0.8f; // Minimum size on Y-axis when HP is low
    public float maxYSize = 2.0f; // Maximum size on Y-axis when HP is full

    private Transform darknessTransform;

    void Start()
    {
        currentHealth = maxHealth; // Set initial health
        darknessTransform = transform; // Get the transform component of the darkness object
        InvokeRepeating(nameof(ReduceHealth), 1f, 1f); // Reduce health every second
    }

    void ReduceHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth -= 5; // Decrease health by 1
            Debug.Log("Current Health: " + currentHealth);

            // Calculate the scale factor based on health percentage
            float healthPercentage = (float)currentHealth / maxHealth;
            float newXSize = Mathf.Lerp(minXSize, maxXSize, healthPercentage);
            float newYSize = Mathf.Lerp(minYSize, maxYSize, healthPercentage);

            // Apply the scale factor to the darkness object
            transform.localScale = new Vector3(newXSize, newYSize, 1f);
        }
    }
}
