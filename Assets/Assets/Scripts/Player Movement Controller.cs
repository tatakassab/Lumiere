using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jump_height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InputController.jump.performed += jump;
    }

    private void OnDisable()
    {
        InputController.jump.performed -= jump;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = InputController.move.ReadValue<Vector2>().x*speed;
    }

    void jump(InputAction.CallbackContext context)
    {
        if(rb.linearVelocityY == 0)
        {
            rb.AddForceY(jump_height);
        }
    }
}
