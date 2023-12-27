using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostController : MonoBehaviour
{
    public int hp = 1000;
    public TMP_Text healthText; 

    public void DecreaseHealth(int amount)
    {
        hp -= amount;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthText != null)
        {
            healthText.text = "Ghost HP: " + hp.ToString();
        }
    }
}
