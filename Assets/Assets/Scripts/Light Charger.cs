using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightCharger : MonoBehaviour
{
    [SerializeField] InputController input;
    private LightSystem playerLight;
    private bool charging = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerLight = collision.GetComponent<LightSystem>();
            input.RegisterToInteract(StartCharging);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerLight = null;
            input.UnregisterFromInteract(StartCharging);
        }
    }

    private void StartCharging(InputAction.CallbackContext context)
    {
        if (playerLight == null) return;
        playerLight.ChangeEnergy(100);
    }
}
