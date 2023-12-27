using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    private float moveSpeed = 5f;
    public float maxHp = 100f;
    public float hp;

    private PlayerState currentState;

    private MoveState moveState;
    private BattleState battleState;

    public Animator anim;
    private float idleTimer = 0f; // Таймер для айдл анімації
    private float idleAnimationDelay = 5f; // Затримка перед початком айдл анімації (1 хвилина = 60 секунд)
    private float nextAnimationDelay = 5f; // Затримка перед початком наступної анімації

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject attackZone;

    public TMP_Text healthBarText;
    private bool isInBattleState = false;
    private bool isMoving; // Нова змінна для визначення руху гравця

    private void Start()
    {
        battleState = new BattleState(this, ghost, attackZone);
        moveState = new MoveState(this);
        hp = maxHp;

        TransitionToState(moveState);
        UpdateHealthBar();
    }

    private void Update()
    {
        currentState.UpdateState();
        currentState.HandleMovement();

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!isInBattleState)
                TransitionToState(battleState);
            else
                TransitionToState(moveState);

            isInBattleState = !isInBattleState;
        }

        if (!isAlive())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Перевірка на рух гравця
        isMoving = IsPlayerMoving();

        // Передача значення isMoving в аніматор
        anim.SetBool("IsMoving", isMoving);

        // Оновлення часу для початку айдл анімації лише, якщо гравець не рухається
        if (!isMoving)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleAnimationDelay)
            {
                anim.SetFloat("IdleTimer", 1f); // Оновлюємо параметр IdleTimer в аніматорі
                StartCoroutine(NextAnimationDelayCoroutine()); // Запуск корутини для запуску наступної анімації через nextAnimationDelay
            }
        }
        else
        {
            idleTimer = 0f; // Скидаємо таймер, якщо гравець рухається
            anim.SetFloat("IdleTimer", 0f); // Скидаємо параметр IdleTimer в аніматорі
        }
    }

    private IEnumerator NextAnimationDelayCoroutine()
    {
        yield return new WaitForSeconds(nextAnimationDelay);
        // Тут ви можете вказати логіку для наступної анімації, наприклад:
        // anim.SetTrigger("NextAnimation"); // Запуск наступної анімації за допомогою тригера
        Debug.Log("Next animation after " + nextAnimationDelay + " seconds");
    }

    public void TransitionToState(PlayerState state)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = state;
        currentState.EnterState();
    }

    public void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public Vector2 GetMovementDirection()
    {
        return movement.normalized;
    }

    public bool IsPlayerMoving()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }

    public void GetDamage(float damage)
    {
        if (hp - damage >= 0)
        {
            hp -= damage;
        }
        else
        {
            hp = 0;
        }
        UpdateHealthBar();
    }

    public bool isAlive()
    {
        return hp > 0;
    }

    public void UpdateHealthBar()
    {
        healthBarText.text = "HP: " + hp.ToString("0") + "/" + maxHp.ToString("0");
    }
}