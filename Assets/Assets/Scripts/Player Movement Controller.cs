using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] InputController inputs;
    [SerializeField] float speed;
    [SerializeField] float jump_height;

    private Rigidbody2D rb;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs.RegisterToJump(jump);
    }

    private void OnEnable()
    {
        //inputs.RegisterToJump(jump);
    }

    private void OnDisable()
    {
        inputs.UnregisterFromJump(jump);
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        move(inputs);
    }

    void move(IInputAxisController axis)
    {
        rb.linearVelocityX = axis.GetMoveReading().x * speed;
    }

    void jump(InputAction.CallbackContext context)
    {
        if(rb.linearVelocityY == 0)
        {
            rb.AddForceY(jump_height);
        }
    }
}
