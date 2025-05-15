using UnityEngine;
using UnityEngine.InputSystem;

public class MovingPlayerState : IPlayerState
{
    private float maxSpeed;
    private InputController inputs;
    private Rigidbody2D rb;
    private PlayerStateMachine player;
    private bool direction;
    private AudioClip movingSound;
    private AudioSource movingSource;

    public MovingPlayerState(PlayerStateMachine player)
    {
        this.player = player;
        inputs = player.GetInputController();
        rb = player.GetRigidbody();
        maxSpeed = player.GetMaxSpeed();
        movingSound = player.GetMovingSound();
        movingSource = player.GetAudioSource();
    }

    public void EnterState()
    {
        inputs.RegisterToJump(jump);
        direction = rb.linearVelocityX > 0;
        movingSource.loop = true;
        movingSource.clip = movingSound;
        movingSource.Play();
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
        movingSource.loop = false;
        movingSource.clip = null;
        movingSource.Stop();
        inputs.UnregisterFromJump(jump);
    }
}
