using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChasePlayer : MonoBehaviour
{
    PlayerMovement _player;
    private NavMeshAgent _agent;
    CamShake _camShakeHandle;

    [SerializeField] private Slider _healthBar;

    [SerializeField] private int _enemyHealth;
    [SerializeField] private int _damageReceivedByEnemy;
    [SerializeField] private int _damageGivenToPlayer;


    [SerializeField] private float knockBackDuration;
    [SerializeField] private float knockBackForce;

    private DropLoot loot;

    void Awake()
    {
        loot = GetComponent<DropLoot>();
        _camShakeHandle = GameObject.Find("Main Camera").GetComponent<CamShake>();
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private Vector3 lastFramePosition;
    private Vector3 knockBackDirection;

    void Update()
    {
        SendEnemyToPlayer();
        DisplayHealth();

        lastFramePosition = transform.position;
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
            _player.Damage(_damageGivenToPlayer, knockBackDirection, knockBackForce, knockBackDuration);
            _camShakeHandle.EnableShake();
            //sfx here
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player_Bullet"))
        {
            //Spawn a particle effect of to show bullet impact.
            EnemyDamage(_damageReceivedByEnemy);
            _camShakeHandle.EnableShake();
            Destroy(other.gameObject);

        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            //Spawn a particle effect of to show bullet impact.
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
            loot.LootProbability();
            Destroy(gameObject);
        }
        
    }

    void DisplayHealth()
    {
       _healthBar.value = _enemyHealth;
       _healthBar.gameObject.transform.rotation = Quaternion.identity;
    }
}