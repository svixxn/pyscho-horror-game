using UnityEngine;

public class BattleState : PlayerState
{
    private GameObject ghost;
    private GameObject attackZone;
    private Transform pivot;
    private Quaternion defaultRotation;



    public BattleState(PlayerController playerController, GameObject ghost, GameObject attackZone) : base(playerController)
    {
        this.ghost = ghost;
        this.attackZone = attackZone;
        this.pivot = attackZone.transform.parent;
        defaultRotation = pivot.rotation;
    }

    public override void EnterState()
    {
        if (ghost != null && attackZone != null)
        {
            ghost.SetActive(true);
            attackZone.SetActive(true);

        }
    }

    public override void UpdateState()
    {
        if (playerController.IsPlayerMoving())
        {
            RotateAttackZone();
        }
        else 
        {
            pivot.rotation = defaultRotation;
        }
    }

    private void RotateAttackZone()
    {
        Vector2 movementDirection = playerController.GetMovementDirection(); 
        float angle = Mathf.Atan2(movementDirection.x, -movementDirection.y) * Mathf.Rad2Deg; 
        pivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void ExitState()
    {
        if (ghost != null)
        {
            ghost.SetActive(false);
            attackZone.SetActive(false);
        }
    }
}
