using Unity.Mathematics;
using UnityEngine;

public class RisingJumpPlayerState : IPlayerState
{
    private InputController inputs;
    private Rigidbody2D rb;
    private PlayerStateMachine player;
    private float airSpeed;
    public RisingJumpPlayerState(PlayerStateMachine player)
    {
        this.player = player;
        inputs = player.GetInputController();
        rb = player.GetRigidbody();
        airSpeed = player.GetAirControl();
    }
    public void EnterState()
    {
        
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        if(rb.linearVelocityY < 0)
        {
            player.TransitionToState(new FallingPlayerState(player));
            return;
        }

        if (inputs.GetMoveReading().x < 0 && rb.linearVelocityX <= 1)
        {
            rb.linearVelocityX = math.min(-airSpeed, rb.linearVelocityX);
        }
        else if (inputs.GetMoveReading().x > 0 && rb.linearVelocityX >= -1)
        {
            rb.linearVelocityX = math.max(airSpeed, rb.linearVelocityX);
        }
    }
}
