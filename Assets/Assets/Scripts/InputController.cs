using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private static InputSystem_Actions Actions;
    public static InputAction move;
    public static InputAction jump;

    private void Awake()
    {
        Actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        move = Actions.Player.Move;
        move.Enable();

        jump = Actions.Player.Jump;
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }
}
