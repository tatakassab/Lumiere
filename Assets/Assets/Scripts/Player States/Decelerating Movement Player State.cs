using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeceleratingMovementPlayerState : IPlayerState
{
    private float deceleration;
    private InputController inputs;
    private Rigidbody2D rb;
    private PlayerStateMachine player;

    public DeceleratingMovementPlayerState(PlayerStateMachine player)
    {
        this.player = player;
        inputs = player.GetInputController();
        rb = player.GetRigidbody();
        deceleration = player.GetDeceleration();
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

        if (inputs.GetMoveReading().x != 0)
        {
            player.TransitionToState(new AcceleratingMovementPlayerState(player));
            return;
        }

        if (rb.linearVelocityX != 0)
        {
            float newSpeed;
            if (rb.linearVelocityX > 0) {
                newSpeed = rb.linearVelocityX - deceleration * Time.deltaTime;
            }
            else
            {
                newSpeed = rb.linearVelocityX + deceleration * Time.deltaTime;
            }

            if (math.abs(newSpeed) > math.abs(rb.linearVelocityX))
            {
                newSpeed = 0;
            }

            rb.linearVelocityX = newSpeed;
        }
        else
        {
            player.TransitionToState(new IdlePlayerState(player));
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
