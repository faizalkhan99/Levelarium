using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChasePlayer : MonoBehaviour
{
    PlayerMovement _player;
    private NavMeshAgent _agent;
    [SerializeField] private Slider _healthBar;

    [SerializeField] private float _enemyHealth;
    [SerializeField] private float _enemyDamage;

    [SerializeField] Bullet _bullet;
    void Awake()
    {
        //_bullet = FindObjectOfType<Bullet>();
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {

    }
    void Update()
    {
        _agent.destination = _player.transform.position;
        DisplayHealth();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //player damage
            _player.Damage(1f);
            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            //damage enemy
            EnemyDamage(_enemyDamage);
            Destroy(other.gameObject);

        }
           
    }
    public void EnemyDamage(float damage)
    {
         _enemyHealth -= damage; //dry karle ise
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