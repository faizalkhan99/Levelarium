using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChasePlayer : MonoBehaviour
{
    PlayerMovement _player;
    private NavMeshAgent _agent;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _damage;
    [SerializeField] private float _health;
    [SerializeField] private Slider _healthBar;
    void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _targetTransform = _player.transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _health = 1.0f;
    }
    void Update()
    {
        _agent.destination = _targetTransform.position;
        DisplayHealth();
        CheckIfDead();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.Damage(_damage);
            Destroy(gameObject, 0.1f);
        }
    }

    void DisplayHealth()
    {
        _healthBar.value = _health;
    }
    public void DamageEnemy(float damage)
    {
        _health -= damage;
    }

    public void CheckIfDead()
    {
        if(_health <= 0)
        {
            Destroy(gameObject,0.1f);
        }
    }
}