using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LightDetector : MonoBehaviour
{
    [SerializeField] UnityEvent switchEvent;
    [SerializeField] float energyRequired = 20f;
    [SerializeField] InputController control;
    private bool isActivated = false;
    private LightSystem playerLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerLight = collision.GetComponent<LightSystem>();

            if (playerLight == null || control == null)
            {
                Debug.LogError("Player does not have a LightSystem script or a controller.");
                return;
            }

            control.RegisterToInteract(InteractWithSwitch);
        }
    }

    private void InteractWithSwitch(InputAction.CallbackContext context)
    {
        if (isActivated || playerLight == null) return;
        if (playerLight.ChangeEnergy(-energyRequired))
        {
            ActivateSwitch();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        control.UnregisterFromInteract(InteractWithSwitch);
    }

    private void ActivateSwitch()
    {
        isActivated = true;
        switchEvent.Invoke();
        Debug.Log("Switch Activated!");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerLight = null;
        }
    }
}
