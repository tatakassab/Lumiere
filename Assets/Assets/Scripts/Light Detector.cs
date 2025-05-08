using UnityEngine;
using UnityEngine.Events;

public class LightDetector : MonoBehaviour
{
    [SerializeField] UnityEvent switchEvent;
    [SerializeField] float energyRequired = 20f;
    private bool isActivated = false;
    private LightSystem playerLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerLight = collision.GetComponent<LightSystem>();

            if (playerLight == null)
            {
                Debug.LogError("Player does not have a LightSystem script.");
                return;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActivated || playerLight == null) return;
        if (playerLight.ChangeEnergy(-energyRequired))
        {
            ActivateSwitch();
        }
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
