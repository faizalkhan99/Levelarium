using UnityEngine;

public class Pressureplate : MonoBehaviour
{
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
    }
}

