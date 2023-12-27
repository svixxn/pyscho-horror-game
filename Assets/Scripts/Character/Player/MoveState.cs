using UnityEngine;

public class MoveState : PlayerState
{

    public MoveState(PlayerController playerController) : base(playerController)
    {
    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    { 
       
    }

    public override void ExitState()
    {
        // Stop player movement or perform any necessary cleanup
    }
}
