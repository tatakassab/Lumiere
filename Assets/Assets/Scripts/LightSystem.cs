using UnityEngine;
using UnityEngine.InputSystem;

public class LightSystem : MonoBehaviour
{
    //intellisense
    [SerializeField] GameObject light;
    [SerializeField] InputController input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input.RegisterToLight(switchLight);
    }

    void OnEnable()
    {
        //input.RegisterToLight(switchLight);
    }

    void OnDisable()
    {
        input.UnregisterFromLight(switchLight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void switchLight(InputAction.CallbackContext context){
        if(light.activeInHierarchy){
            light.SetActive(false);
        }
        else{
            light.SetActive(true);
        }
    }
}
