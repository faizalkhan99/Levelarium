using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private string gateTag;

    private void OnTriggerEnter(Collider other)
    {
        
            GateController gateController = GameObject.FindGameObjectWithTag(gateTag)?.GetComponent<GateController>();
            if (gateController != null)
            {
                gateController.OnPressurePlateActivated();
            }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
            GateController gateController = GameObject.FindGameObjectWithTag(gateTag)?.GetComponent<GateController>();
            if (gateController != null)
            {
                gateController.OnPressurePlateDeactivated();
            }
        
    }

    /*
    [SerializeField] private Transform _gate;
    private void Start()
    {
        OpenGate(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        OpenGate(false);
    }
    private void OnTriggerExit(Collider other)
    {
        OpenGate(true);
    }
    private void OpenGate(bool condition)
    {
        if (_gate)
        {
            _gate.gameObject.SetActive(condition);
        }
    }*/
}

