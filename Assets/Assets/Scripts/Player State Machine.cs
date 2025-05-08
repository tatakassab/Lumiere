using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TransitionToState(new IdlePlayerState(this));
    }

    private void Update()
    {
        currentState.UpdateState();
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

    public float GetMaxSpeed() => maxSpeed;
    public float GetAcceleration() => acceleration;
    public float GetDeceleration() => deceleration;
    public float GetJumpForce() => jumpForce;
    public float GetMaxJumpTime() => maxInitialJumpTime;
    public float GetAirControl() => airControl;
    public InputController GetInputController() => inputs;
    public Rigidbody2D GetRigidbody() => rb;

    public bool IsGrounded()
    {
        return Physics2D.BoxCast((Vector2)transform.position - groundBoxOffset, groundBoxSize, 0, -transform.up, goundBoxCastDistance, groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector3)((Vector2)transform.position - groundBoxOffset) - transform.up* goundBoxCastDistance, groundBoxSize);
    }
}
