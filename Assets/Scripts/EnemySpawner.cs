using System.Collections;
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

    [SerializeField] private Slider _healthBar;

    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] GameObject _enemyPrefab;

    [SerializeField] EnemyChasePlayer _ECP;

    private void Awake()
    {
        _camShake = GameObject.Find("Main Camera").GetComponent<CamShake>();
    }
    private void Start()
    {
        _ECP.InitializeEnemyHealth(_enemyHealth, _damageDealtByenemy, _damageGivenByEnemy);
        StartCoroutine(SpanwEnemies());
    }
    IEnumerator SpanwEnemies()
    {
        while (gameObject)
        {
            yield return new WaitForSeconds(_timeBetweenSpawn);
            Instantiate(_enemyPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
        }
    }

    private void Update()
    {
        DisplayHealth();
    }

    void DisplayHealth() => _healthBar.value = _health;
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            _health -= _damageDealt;
            if(_health <= 0) Die();
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