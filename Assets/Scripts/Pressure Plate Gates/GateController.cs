using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private int activatedPressurePlates = 0;
    [SerializeField] private int totalPressurePlates;

    [SerializeField] private AudioClip _gateOpenSFX;
    [SerializeField] private AudioClip _gateCloseSFX;

    private void Start()
    {
        totalPressurePlates = transform.childCount;
        OpenGate(true); 
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
            AudioManager.Instance.PlaySFX(_gateCloseSFX);
            OpenGate(true);
        }
    }

    private void OpenGate(bool condition)
    {
        if (gate)
        {
            if (condition)
            {
                gate.SetActive(condition);
            }
            else
            {
                AudioManager.Instance.PlaySFX(_gateOpenSFX);
                gate.SetActive(condition);
            }
        }
    }
}
