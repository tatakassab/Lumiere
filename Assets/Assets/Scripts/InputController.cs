using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputRegistrationManager
{
    void RegisterToJump(Action<InputAction.CallbackContext> action);

    void UnregisterFromJump(Action<InputAction.CallbackContext> action);
}

public interface IInputAxisController
{
    Vector2 GetMoveReading();
}


public class InputController : MonoBehaviour, IInputRegistrationManager, IInputAxisController
{
    InputSystem_Actions Actions;
    InputAction move;
    InputAction jump;

    private void Awake()
    {
        Actions = new InputSystem_Actions();
        move = Actions.Player.Move;
        jump = Actions.Player.Jump;
    }

    private void OnEnable()
    {
        move.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    public void RegisterToJump(Action<InputAction.CallbackContext> action)
    {
        jump.performed += action;
    }

    public void UnregisterFromJump(Action<InputAction.CallbackContext> action)
    {
        jump.performed -= action;
    }

    public Vector2 GetMoveReading() => move.ReadValue<Vector2>();


    public InputAction Move => move;

}
