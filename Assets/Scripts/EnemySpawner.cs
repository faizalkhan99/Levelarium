using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemySpawnerHealth _spawnerHealth;
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] GameObject _enemyPrefab;  

    private void Awake()
    {
        _spawnerHealth = FindObjectOfType<EnemySpawnerHealth>();
    }
    private void Start()
    {
        StartCoroutine(SpanwEnemies());
    }
    IEnumerator SpanwEnemies()
    {
        while (gameObject)
        {
            Instantiate(_enemyPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
        }
    }

    private void Update()
    {
        if(_spawnerHealth._health <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}


/*public interface HEALTH
{ 
    void Damage();
}*/