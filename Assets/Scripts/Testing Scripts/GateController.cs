using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private int activatedPressurePlates = 0;
    [SerializeField] private int totalPressurePlates;

    private void Start()
    {
        totalPressurePlates = transform.childCount;
        OpenGate(true); // Start with the gate closed
    }

    public void OnPressurePlateActivated()
    {
        activatedPressurePlates++;
        UpdateGateState();
    }

    public void OnPressurePlateDeactivated()
    {
        activatedPressurePlates--;
        UpdateGateState();
    }
    private int _counter = 0;
    [SerializeField] GameObject[] _enemyToSpawn;
    [SerializeField] Transform[] _position;
    private void UpdateGateState()
    {
        // Open the gate only if all assigned pressure plates are activated
        if (activatedPressurePlates >= totalPressurePlates)
        {
            OpenGate(false);
            if(_counter == 0)
            {
                _counter = 1;
                for (int i = 0; i < _enemyToSpawn.Length && _enemyToSpawn[i]; i++)
                {
                    Instantiate(_enemyToSpawn[i], _position[i].position, Quaternion.identity);
                }
            }
        }
        else
        {
            OpenGate(true);
        }
    }

    private void OpenGate(bool open)
    {
        if (gate)
        {
            gate.SetActive(open);
        }
    }
}
