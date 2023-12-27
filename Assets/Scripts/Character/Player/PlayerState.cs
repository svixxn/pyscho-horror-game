public abstract class PlayerState
{
    protected PlayerController playerController;

    public PlayerState(PlayerController playerController)
    {
        this.playerController = playerController;
    }


    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

    public virtual void HandleMovement()
    {
        playerController.Move(); 
    }

}