using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class AcceleratingMovementPlayerState: IPlayerState
{
    private float maxSpeed;
    private float acceleration;
    private InputController inputs;
    private Rigidbody2D rb;
    private PlayerStateMachine player;

    public AcceleratingMovementPlayerState(PlayerStateMachine player)
    {
        this.player = player;
        inputs = player.GetInputController();
        rb = player.GetRigidbody();
        maxSpeed = player.GetMaxSpeed();
        acceleration = player.GetAcceleration();
    }

    public void EnterState()
    {
        inputs.RegisterToJump(jump);
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

        

        if ((rb.linearVelocityX < maxSpeed && inputs.GetMoveReading().x > 0) || (rb.linearVelocityX > -maxSpeed && inputs.GetMoveReading().x < 0))
        {
            rb.linearVelocityX = rb.linearVelocityX + inputs.GetMoveReading().x * acceleration * Time.deltaTime;
        }
        else
        {
            player.TransitionToState(new MovingPlayerState(player));
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
