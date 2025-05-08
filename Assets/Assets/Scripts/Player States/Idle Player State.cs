using UnityEngine;
using UnityEngine.InputSystem;

public class IdlePlayerState : IPlayerState
{
    private PlayerStateMachine player;
    private InputController inputs;
    private Rigidbody2D rb;

    public IdlePlayerState(PlayerStateMachine player)
    {
        this.player = player;
        inputs = player.GetInputController();
        rb = player.GetRigidbody();
    }

    public void EnterState()
    {
        inputs.RegisterToJump(jump);
    }

    public void UpdateState()
    {
        if(!player.IsGrounded())
        {
            player.TransitionToState(new FallingPlayerState(player));
            return;
        }

        if (inputs.GetMoveReading().x != 0)
        {
            player.TransitionToState(new AcceleratingMovementPlayerState(player));
            return;
        }

        rb.linearVelocityX = 0;
        rb.linearVelocityY = 0;
    }

    void jump(InputAction.CallbackContext context)
    {
        player.TransitionToState(new StartingJumpPlayerState(player));
    }

    public void ExitState()
    {
        inputs.UnregisterFromJump(jump);
    }
}
