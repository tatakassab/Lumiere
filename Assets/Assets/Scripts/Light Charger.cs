using UnityEngine;

public class LightCharger : MonoBehaviour
{
    private LightSystem playerLight;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerLight = collision.GetComponent<LightSystem>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerLight == null) return;
        playerLight.ChangeEnergy(100);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerLight = null;
        }
    }
}
