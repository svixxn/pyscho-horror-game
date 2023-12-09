using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    private float moveSpeed = 5f;

    private PlayerState currentState;

    private MoveState moveState;
    private BattleState battleState;


    public Animator anim;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject attackZone;


    private bool isInBattleState = false;


    private void Start()
    {
        battleState = new BattleState(this, ghost, attackZone);
        moveState = new MoveState(this);

        TransitionToState(moveState);
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

}