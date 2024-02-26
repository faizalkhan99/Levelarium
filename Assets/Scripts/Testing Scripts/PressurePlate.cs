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
}

