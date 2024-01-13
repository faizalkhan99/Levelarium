using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChasePlayer : MonoBehaviour
{
    PlayerMovement _player;
    CamShake _camShakeHandle;
    private NavMeshAgent _agent;

    [SerializeField] private Slider _healthBar;

    [SerializeField] private int _enemyHealth;
    [SerializeField] private int _damageReceivedByEnemy;
    [SerializeField] private int _damageGivenByEnemy;
    [SerializeField] private float knockBackDuration;
    [SerializeField] private float knockBackForce;

    public void InitializeEnemyHealth(int health, int damageDealt, int damageGiven)
    {
        _enemyHealth = health;
        _damageReceivedByEnemy = damageDealt;
        _damageGivenByEnemy = damageGiven;
    }
    void Awake()
    {
        _camShakeHandle = GameObject.Find("Main Camera").GetComponent<CamShake>();
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private Vector3 lastFramePosition;
    private Vector3 knockBackDirection;
    private void Start()
    {
        lastFramePosition = transform.position;
    }

    void Update()
    {
        SendEnemyToPlayer();
        DisplayHealth();
        _healthBar.gameObject.transform.rotation = Quaternion.identity;

        Vector3 movementDirection = transform.position - lastFramePosition;

        // Update knockBackDirection based on movement direction
        knockBackDirection = movementDirection.normalized;

        // Save current position for the next frame
        lastFramePosition = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.Damage(_damageGivenByEnemy, knockBackDirection, knockBackForce, knockBackDuration);
            _camShakeHandle.EnableShake();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            EnemyDamage(_damageReceivedByEnemy);
            _camShakeHandle.EnableShake();
            Destroy(other.gameObject);

        }
    }

    private void SendEnemyToPlayer()
    {
        if (_agent != null)
        {
            _agent.enabled = true;
            _agent.destination = _player.transform.position;
        }
    }
    public void EnemyDamage(int damage)
    {
         _enemyHealth -= damage; //dry run karle ise
        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    void DisplayHealth()
    {
       _healthBar.value = _enemyHealth;
    }

}