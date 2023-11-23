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
    [SerializeField] private int _enemyDamage;
    [SerializeField] private int _damageGivenByEnemy;

    public void InitializeEnemyHealth(int health, int damageDealt, int damageGiven)
    {
        _enemyHealth = health;
        _enemyDamage = damageDealt;
        _damageGivenByEnemy = damageGiven;
    }
    void Awake()
    {
        _camShakeHandle = GameObject.Find("Main Camera").GetComponent<CamShake>();
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        SendEnemyToPlayer();
        DisplayHealth();
        _healthBar.gameObject.transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.Damage(_damageGivenByEnemy); //player damage
            Destroy(gameObject); //enemy destroy
            _camShakeHandle.EnableShake();
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            //damage enemy
            EnemyDamage(_enemyDamage);
            Destroy(other.gameObject);  //bullet destroy
            _camShakeHandle.EnableShake();

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