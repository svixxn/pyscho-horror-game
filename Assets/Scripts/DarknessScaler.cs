using UnityEngine;

public class DarknessScaler : MonoBehaviour
{
    private int maxPsyche; // Maximum health
    private int currentPsyche; // Current health

    private PsycheBar psycheInstance;

    public float minXSize = 18f; // Minimum size on X-axis when HP is low
    public float maxXSize = 1.5f; // Maximum size on X-axis when HP is full
    public float minYSize = 0.8f; // Minimum size on Y-axis when HP is low
    public float maxYSize = 2.0f; // Maximum size on Y-axis when HP is full66

    private Transform darknessTransform;

    void Start()
    {
        darknessTransform = transform; // Get the transform component of the darkness object
        //–Œ¡»ÃŒ ¬Œ“ “¿ ” ÿ“” ”!!!
        psycheInstance = FindObjectOfType<PsycheBar>();
        maxPsyche = psycheInstance.maxPsyche;
    }

    void Update()
    {
        currentPsyche = psycheInstance.currentPsyche;
        if (currentPsyche > 0)
        {
            // Calculate the scale factor based on health percentage
            float healthPercentage = (float)currentPsyche / maxPsyche;
            float newXSize = Mathf.Lerp(minXSize, maxXSize, healthPercentage);
            float newYSize = Mathf.Lerp(minYSize, maxYSize, healthPercentage);

            // Apply the scale factor to the darkness object
            transform.localScale = new Vector3(newXSize, newYSize, 1f);
        }
    }
}
