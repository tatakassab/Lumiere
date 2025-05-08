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
    InputAction switchLight;

    private void Awake()
    {
        Actions = new InputSystem_Actions();
        move = Actions.Player.Move;
        jump = Actions.Player.Jump;
        switchLight = Actions.Player.Light;
    }

    private void OnEnable()
    {
        move.Enable();
        jump.Enable();
        switchLight.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        switchLight.Disable();
    }

    public void RegisterToJump(Action<InputAction.CallbackContext> action)
    {
        jump.performed += action;
    }

    public void UnregisterFromJump(Action<InputAction.CallbackContext> action)
    {
        jump.performed -= action;
    }

    public bool IsStillJumping()
    {
        return jump.IsPressed();
    }


    public void RegisterToLight(Action<InputAction.CallbackContext> action)
    {
        switchLight.performed += action;
    }

    public void UnregisterFromLight(Action<InputAction.CallbackContext> action)
    {
        switchLight.performed -= action;
    }


    public Vector2 GetMoveReading() => move.ReadValue<Vector2>();


    public InputAction Move => move;

}
