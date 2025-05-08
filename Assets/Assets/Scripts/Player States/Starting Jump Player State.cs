using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class StartingJumpPlayerState : IPlayerState
{
    private InputController inputs;
    private Rigidbody2D rb;
    private PlayerStateMachine player;
    private float jumpForce;
    private float maxJumpTime;
    private float currentJumpTime;
    public StartingJumpPlayerState(PlayerStateMachine player)
    {
        this.player = player;
        inputs = player.GetInputController();
        rb = player.GetRigidbody();
        jumpForce = player.GetJumpForce();
        maxJumpTime = player.GetMaxJumpTime();
    }
    public void EnterState()
    {
        rb.linearVelocityY = jumpForce;
        currentJumpTime = maxJumpTime;
    }

    public void ExitState()
    {
    }

    public void UpdateState()
    {
        if (inputs.IsStillJumping() && currentJumpTime > 0)
        {
            rb.linearVelocityY = jumpForce;
            currentJumpTime -= Time.deltaTime;
        }
        else
        {
            player.TransitionToState(new RisingJumpPlayerState(player));
        }
    }

    
}
