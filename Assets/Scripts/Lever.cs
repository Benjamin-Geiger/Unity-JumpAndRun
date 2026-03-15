using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class Lever : MonoBehaviour
{
    private bool on = false;
    public UnityEvent<bool> onStateChanged;
    private InputAction interactAction;

    [SerializeField] private Transform onPosition;
    [SerializeField] private Transform offPosition;
    [SerializeField] private GameObject leverHandle;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.interactAction = InputSystem.actions.FindAction("Interact");    
    }

    void ToggleLever()
    {
        on = !on;
        onStateChanged.Invoke(on);
        if (on)
        {
            leverHandle.transform.SetPositionAndRotation(onPosition.position, onPosition.rotation);
        }
        else
        {
            leverHandle.transform.SetPositionAndRotation(offPosition.position, offPosition.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.interactAction.WasPressedThisFrame())
        {
            ToggleLever();
        }
    }
}
