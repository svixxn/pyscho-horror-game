using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public Animator anim;
    Vector2 movement;

    [SerializeField] private Rigidbody2D rb;

    private DamageOverDistance damageScript;

    public PsycheBar psycheBar; // Посилання на скрипт полоси здоров'я

    private void Start()
    {

        //// Отримати позицію гравця з GameData
        //Vector3 playerPosition = GameData.playerPosition;

        //// Встановити позицію гравця на основі отриманих даних
        //transform.position = playerPosition;

        //psycheBar.SetMaxPsyche(maxPsyche);
        //psycheBar.SetPsyche(currentPsyche);
        damageScript = GetComponent<DamageOverDistance>();
        damageScript.SetPsycheBar(psycheBar); // Додайте цей рядок
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (damageScript != null)
            {
                damageScript.ApplyDamage(20); // Виклик методу ApplyDamage з екземпляра DamageOverDistance, який належить PsycheBar
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
