using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SeeAndFollow : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject _player;
    CamShake _camShakeHandle;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private int _health;
    [SerializeField] private int _damageGiven;
    [SerializeField] private int _damageRecieved;
    [SerializeField] private int _knockbackForce;
    [SerializeField] private int _knockBackDuration;

    [SerializeField] private bool _followPlayer = false;
    private void Awake()
    {
        _camShakeHandle = GameObject.Find("Main Camera").GetComponent<CamShake>();
        _player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(_followPlayer && _player != null)
        {
            agent.SetDestination(_player.transform.position);
        }
        DisplayHealth();
    }
    void DisplayHealth()
    {
        _healthBar.value = _health;
        _healthBar.gameObject.transform.rotation = Quaternion.identity;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _followPlayer = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement p = _player.gameObject.GetComponent<PlayerMovement>();
            p.Damage(_damageGiven, gameObject.transform.forward, _knockbackForce, _knockBackDuration);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player_Bullet"))
        {
            Destroy(collision.gameObject);
            Damage();
        }
    }

    void Damage()
    {
        _health = _health - _damageRecieved;
        if(_health <= 0)
        {
            //play damage sfx here.
            Die();
        }
    }
    void Die()
    {
        _camShakeHandle.EnableShake();
        Destroy(gameObject);
    }
}
