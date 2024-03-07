using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PatrolAndFollowShoot : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] Transform currentWaypoint;

    [SerializeField] int currentWaypointIndex = 0;
    public float pauseTime = 0.5f;
    private float _timer = 0f;
    [SerializeField] private bool _isMoving = true;

    CamShake _camShakeHandle;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private int _enemyHealth;
    [SerializeField] private int _damageReceivedByEnemy;
    private NavMeshAgent _agent;

    Shoot _shoot;
    private Coroutine _shootCoroutine;
    [SerializeField] private bool _shooting = false;
    private void Awake()
    {
        _camShakeHandle = GameObject.Find("Main Camera").GetComponent<CamShake>();
        _agent = GetComponent<NavMeshAgent>();
        _shoot = GetComponent<Shoot>();
    }
    void Start()
    {
        if (wayPoints.Length > 0) currentWaypoint = wayPoints[currentWaypointIndex];
    }
    private void Update()
    {
        //DisplayHealth();
    }
    void FixedUpdate()
    {
        HandleMovement();
    }
    [SerializeField] private Transform _player;
    [SerializeField] private bool _playerInsideTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _shooting = true;
            _shootCoroutine = StartCoroutine(ShootAtPlayer());
            _agent.stoppingDistance += 6;
            _playerInsideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_shootCoroutine != null)
            {
                StopCoroutine(ShootAtPlayer());
                _shootCoroutine = null;
            }
            _shooting = false;
            _agent.stoppingDistance -= 6;
            _playerInsideTrigger = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player_Bullet"))
        {
            EnemyDamage(_damageReceivedByEnemy);
        }
    }
    private IEnumerator ShootAtPlayer()
    {
        while (_shooting)
        {
            yield return new WaitForSeconds(2.0f);
            if (_shoot) _shoot.FireBullet();
        }
    }
    void HandleMovement()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            Vector3 targetPosition;
            if (_playerInsideTrigger)
            {
                targetPosition = _player.position;
            }
            else
            {
                targetPosition = new(wayPoints[currentWaypointIndex].position.x, wayPoints[currentWaypointIndex].position.y, wayPoints[currentWaypointIndex].position.z);
            }
            _agent.SetDestination(targetPosition);
            if (Vector3.Distance(transform.position, currentWaypoint.position) < _agent.stoppingDistance)
            {
                _timer = pauseTime;
                SwitchWaypoint();
            }
        }
    }
    void SwitchWaypoint()
    {
        if (currentWaypointIndex == wayPoints.Length - 1)
        {
            currentWaypointIndex = 0;
        }
        else
        {
            currentWaypointIndex++;
        }
        currentWaypoint = wayPoints[currentWaypointIndex];
    }
    void DisplayHealth()
    {
        _healthBar.value = _enemyHealth;
        _healthBar.gameObject.transform.rotation = Quaternion.identity;
    }
    public void EnemyDamage(int damage)
    {
        _enemyHealth -= damage; //dry run karle ise
        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
}
