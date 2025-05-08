using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightSystem : MonoBehaviour
{
    [SerializeField] float maxEnergy = 100f;
    [SerializeField] float energyConsumptionRate = 5f;
    [SerializeField] float lightMinIntensity = 0f;
    [SerializeField] float lightMaxIntensity = 1f;
    [SerializeField] Slider UILight;
    [SerializeField] Light2D playerLight;
    private float currentEnergy;

    private void Start()
    {
        playerLight = GetComponentInChildren<Light2D>();
        currentEnergy = maxEnergy;
        UpdateEnergyBar();
    }

    private void Update()
    {
        ConsumeEnergy();
        UpdateLightIntensity();
        UpdateEnergyBar();
        CheckForDeath();
    }

    private void ConsumeEnergy()
    {
        if (currentEnergy > 0)
        {
            currentEnergy -= energyConsumptionRate * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        }
    }

    private void UpdateLightIntensity()
    {
        if (playerLight != null)
        {
            float normalizedEnergy = currentEnergy / maxEnergy;
            playerLight.intensity = Mathf.Lerp(lightMinIntensity, lightMaxIntensity, normalizedEnergy);
        }
    }

    private void UpdateEnergyBar()
    {
        if (UILight != null)
        {
            UILight.value = currentEnergy;
        }
    }

    public bool ChangeEnergy(float amount)
    {
        if(currentEnergy == 0)
        {
            return false;
        }
        currentEnergy += amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        UpdateEnergyBar();
        return true;
    }

    private void CheckForDeath()
    { 
        if (currentEnergy == 0)
        {
            GameManager.instance.PlayerDied();
        } 
    }
}
