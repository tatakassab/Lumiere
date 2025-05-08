using UnityEngine;
using UnityEngine.InputSystem;

public class MovingPlayerState : IPlayerState
{
    private float maxSpeed;
    private InputController inputs;
    private Rigidbody2D rb;
    private PlayerStateMachine player;
    private bool direction;

    public MovingPlayerState(PlayerStateMachine player)
    {
        this.player = player;
        inputs = player.GetInputController();
        rb = player.GetRigidbody();
        maxSpeed = player.GetMaxSpeed();
    }

    public void EnterState()
    {
        inputs.RegisterToJump(jump);
        direction = rb.linearVelocityX > 0;
    }

    public void UpdateState()
    {
        if (rb.linearVelocityY != 0)
        {
            player.TransitionToState(new IdlePlayerState(player));
            return;
        }

        if (inputs.GetMoveReading().x == 0)
        {
            player.TransitionToState(new DeceleratingMovementPlayerState(player));
            return;
        }

        if(inputs.GetMoveReading().x > 0)
        {
            if(!direction)
            {
                player.TransitionToState(new AcceleratingMovementPlayerState(player));
                return;
            }
            rb.linearVelocityX = maxSpeed;
        }
        else
        {
            if (direction)
            {
                player.TransitionToState(new AcceleratingMovementPlayerState(player));
                return;
            }
            rb.linearVelocityX = -maxSpeed;
        }
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
