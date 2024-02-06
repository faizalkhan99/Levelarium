using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    CamShake _camShake;

    [SerializeField] private GameObject _keyPrefab;

    [SerializeField] private int _health;
    [SerializeField] private int _damageDealt;

    [SerializeField] private int _enemyHealth;
    [SerializeField] private int _damageDealtByenemy;
    [SerializeField] private int _damageGivenByEnemy;

    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private bool _spawningEnemies= false;

    [SerializeField] private Slider _healthBar;

    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _gate;


    [SerializeField] EnemyChasePlayer _ECP;

    private void Awake()
    {
        _gate.SetActive(false);
        _camShake = GameObject.Find("Main Camera").GetComponent<CamShake>();
    }
    private void Start()
    {
        _ECP.InitializeEnemyHealth(_enemyHealth, _damageDealtByenemy, _damageGivenByEnemy);
    }
    IEnumerator SpanwEnemies()
    {
        while (_spawningEnemies)
        {
            Debug.Log("Spawn kar BC");
            yield return new WaitForSeconds(_timeBetweenSpawn);
            Instantiate(_enemyPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
        }
    }

    private void Update()
    {
        DisplayHealth();
    }

    void DisplayHealth() => _healthBar.value = _health;

    private void OnTriggerEnter(Collider other)
    {
        _gate.SetActive(true);
        _spawningEnemies = true;
        StartCoroutine(SpanwEnemies());
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _health -= _damageDealt;
            Destroy(other.gameObject);
            if (_health <= 0)
            {
                _spawningEnemies = false;
                _gate.SetActive(false);
                Die();
            }
        }
    }
    bool temp = false;
    public void Die()
    {
        GameManager.Instance.IsSpawnerDead = true;
        if (temp == false)
        {
            GameManager.Instance.InstantiateKey();
            temp = true;
            _camShake.EnableShake();
            Destroy(gameObject);
        }
        //play destroy sound here
    }
}