using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] _spawnPositions;
    private bool _isDead = false;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private float _health;
    [SerializeField] private Slider _healthSlider;
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    void Update()
    {
        DisplayHealth();
        CheckIfDead();
    }

    IEnumerator SpawnEnemy()
    {
        while (!_isDead)
        {
            Instantiate(_enemyPrefab, _spawnPositions[Random.Range(0,_spawnPositions.Length)].position,Quaternion.identity);
            yield return new WaitForSeconds(_timeBetweenSpawn);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject, 0.1f);
            _health -= 0.06f;
        }
    }
    void DisplayHealth()
    {
        _healthSlider.value = _health;
    }
    void CheckIfDead()
    {
        if(_health <= 0.0000f)
        {
            StopSpawning();
        }
    }
    void StopSpawning()
    {
        _isDead = true;
        
    }
}
