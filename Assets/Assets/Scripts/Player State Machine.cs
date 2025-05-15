using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(SpriteRenderer))]
public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState currentState;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] float jumpForce;
    [SerializeField] float maxInitialJumpTime;
    [SerializeField] float airControl;
    [SerializeField] Vector2 groundBoxSize;
    [SerializeField] Vector2 groundBoxOffset;
    [SerializeField] float goundBoxCastDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] InputController inputs;
    [SerializeField] AudioClip movingSound;
    [SerializeField] AudioClip jumpingSound;
    [SerializeField] AudioClip gruntSound;
    private Animator _animator;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        TransitionToState(new IdlePlayerState(this));
    }

    private void Update()
    {
        currentState.UpdateState();
        if(rb.linearVelocityX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.linearVelocityX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void TransitionToState(IPlayerState newState)
    {
        Debug.Log("Entering " + newState);
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }

    public float GetMaxSpeed() => maxSpeed;
    public float GetAcceleration() => acceleration;
    public float GetDeceleration() => deceleration;
    public float GetJumpForce() => jumpForce;
    public float GetMaxJumpTime() => maxInitialJumpTime;
    public float GetAirControl() => airControl;
    public InputController GetInputController() => inputs;
    public Rigidbody2D GetRigidbody() => rb;
    public AudioClip GetMovingSound() => movingSound;
    public AudioClip GetJumpingSound() => jumpingSound;
    public AudioClip GetGruntSound() => gruntSound;
    public AudioSource GetAudioSource() => audioSource;
    public Animator GetAnimator() => _animator;

    public bool IsGrounded()
    {
        return Physics2D.BoxCast((Vector2)transform.position - groundBoxOffset, groundBoxSize, 0, -transform.up, goundBoxCastDistance, groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector3)((Vector2)transform.position - groundBoxOffset) - transform.up* goundBoxCastDistance, groundBoxSize);
    }
}
