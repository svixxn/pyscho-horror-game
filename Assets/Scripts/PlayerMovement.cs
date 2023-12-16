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

    public PsycheBar psycheBar; // ��������� �� ������ ������ ������'�

    private void Start()
    {

        // �������� ������� ������ � GameData
        Vector3 playerPosition = GameData.playerPosition;

        // ���������� ������� ������ �� ����� ��������� �����
        transform.position = playerPosition;

        //psycheBar.SetMaxPsyche(maxPsyche);
        //psycheBar.SetPsyche(currentPsyche);
        damageScript = GetComponent<DamageOverDistance>();
        damageScript.SetPsycheBar(psycheBar); // ������� ��� �����
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
                damageScript.ApplyDamage(20); // ������ ������ ApplyDamage � ���������� DamageOverDistance, ���� �������� PsycheBar
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
