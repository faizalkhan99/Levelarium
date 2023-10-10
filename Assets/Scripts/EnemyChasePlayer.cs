using UnityEngine.AI;
using UnityEngine;

public class EnemyChasePlayer : MonoBehaviour
{
    PlayerMovement _player;
    private NavMeshAgent _agent;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _damage;
    void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _targetTransform = _player.transform;
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        _agent.destination = _targetTransform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.Damage(_damage);
            Destroy(gameObject, 0.1f);
        }
    }
}