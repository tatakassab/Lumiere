using Unity.Mathematics;
using UnityEngine;

public class FallingPlayerState: IPlayerState
{
    private InputController inputs;
    private Rigidbody2D rb;
    private PlayerStateMachine player;
    private float maxFallSpeed;
    private AudioClip gruntSound;
    public FallingPlayerState(PlayerStateMachine player)
    {
        this.player = player;
        inputs = player.GetInputController();
        rb = player.GetRigidbody();
        maxFallSpeed = player.GetMaxSpeed();
        gruntSound = player.GetGruntSound();
    }
    public void EnterState() { }
    public void UpdateState()
    {
        if (player.IsGrounded())
        {
            player.TransitionToState(new IdlePlayerState(player));
            return;
        }

        if(math.abs(rb.linearVelocityY) < math.abs(maxFallSpeed))
        {
            rb.linearVelocityY = -maxFallSpeed;
        }
    }
    public void ExitState() {
        player.PlaySound(gruntSound);
    }
}
