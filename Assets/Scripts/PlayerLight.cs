using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Light playerLight; // —силка на св≥тло гравц€

    void Start()
    {
        // «найдемо св≥тло гравц€ в доч≥рн≥х об'Їктах гравц€
        playerLight = GetComponentInChildren<Light>();

        if (playerLight != null)
        {
            // ЌалаштуЇмо властивост≥ св≥тла гравц€
            playerLight.range = 10f; // ƒальн≥сть св≥тла
            playerLight.intensity = 1.5f; // ≤нтенсивн≥сть св≥тла
            playerLight.color = Color.yellow; //  ол≥р св≥тла (можна зм≥нити)
        }
    }
}