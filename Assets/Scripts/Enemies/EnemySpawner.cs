using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    CamShake _camShake;

    [SerializeField] private GameObject _keyPrefab;
    [SerializeField] GameObject[] _enemyPrefab;
    [SerializeField] private GameObject _areanaGate;

    [SerializeField] private int _damageDealt;

    [SerializeField] private float _timeBetweenSpawn;

    [SerializeField] private int _health;

    [SerializeField] private Slider _healthBar;

    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] Transform[] _gatesToDisappear;
    [SerializeField] Transform[] _objectsToAppear;


    [SerializeField] private AudioClip _evilMachineDeadSFX;
    [SerializeField] private AudioClip _keySpawnedSFX;


    private void Awake()
    {
        _camShake = GameObject.Find("Main Camera").GetComponent<CamShake>();
    }
    private void Start()
    {
        Gate(false);
        //StartCoroutine(SpanwEnemies());
    }
    public IEnumerator SpanwEnemies()
    {
        while (gameObject)
        {
            yield return new WaitForSeconds(_timeBetweenSpawn);
            Instantiate(_enemyPrefab[Random.Range(0, _enemyPrefab.Length)], _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
            Debug.Log("spawned");
        }
    }

    private void Update()
    {
        DisplayHealth();
    }

    void DisplayHealth() => _healthBar.value = _health;

    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player_Bullet"))
        {
            Debug.Log("Bullet hit");
            _health -= _damageDealt;
            //Spawn a particle effect of to show bullet impact.
            Destroy(other.gameObject);
            if (_health <= 0)
            {
                Gate(false);
                OpenGates();
                ObjectsToAppear();
                Die();
            }
        }   
        if (other.gameObject.CompareTag("Bullet"))
        {   
            //Spawn a particle effect of to show bullet impact.
            Destroy(other.gameObject);
        }
    }
    private bool temp = false;
    public void Die()
    {
        GameManager.Instance.IsSpawnerDead = true;
        if (temp == false)
        {
            GameManager.Instance.InstantiateKey();
            temp = true;
            _camShake.EnableShake();
            AudioManager.Instance.PlaySFX(_evilMachineDeadSFX);
            AudioManager.Instance.PlaySFX(_keySpawnedSFX);
            StopCoroutine(SpanwEnemies());
            Destroy(gameObject);
        }
    }
    public void Gate(bool condition)
    {
        if (_areanaGate)
        {
            _areanaGate.SetActive(condition);
        }
    }
    private void OpenGates()
    {
        for (int i = 0; i < _gatesToDisappear.Length; i++)
        {
            //if gate exists, open it.
            if(_gatesToDisappear[i])
            _gatesToDisappear[i].gameObject.SetActive(false);
        }
    }private void ObjectsToAppear()
    {
        for (int i = 0; i < _gatesToDisappear.Length; i++)
        {
            //if object exists, enable it.
            if(_objectsToAppear[i])
                _objectsToAppear[i].gameObject.SetActive(true);
        }
    }
}