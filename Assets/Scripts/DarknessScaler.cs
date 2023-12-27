using UnityEngine;
using UnityEngine.UI;

public class DarknessScaler : MonoBehaviour
{
    private float maxPsyche; 
    private float currentPsyche;

    private PlayerController playerController;
    public float minXSize = 18f; 
    public float maxXSize = 1.5f; 
    public float minYSize = 0.8f; 
    public float maxYSize = 2.0f; 

    private Transform darknessTransform;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); 
        if (playerController != null)
        {
            maxPsyche = playerController.maxHp; 
            darknessTransform = transform; 
        }
        else
        {
            Debug.LogError("PlayerController not found!");
        }
    }

    void Update()
    {
        currentPsyche = playerController.hp;
        if (currentPsyche > 0)
        {
            float healthPercentage = currentPsyche / maxPsyche;
            float newXSize = Mathf.Lerp(minXSize, maxXSize, healthPercentage);
            float newYSize = Mathf.Lerp(minYSize, maxYSize, healthPercentage);

            transform.localScale = new Vector3(newXSize, newYSize, 1f);
        }
    }
}
