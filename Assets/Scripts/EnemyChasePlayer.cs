using UnityEngine.AI;
using UnityEngine;

public class EnemyChasePlayer : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform _targetTransform;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = _targetTransform.position;
    }
}
