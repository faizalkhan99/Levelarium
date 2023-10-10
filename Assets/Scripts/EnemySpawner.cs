using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damageDealt;

    [SerializeField] private Slider _healthBar;

    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] GameObject _enemyPrefab;
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

        DisplayHealth();
    }

    void DisplayHealth()
    {
        _healthBar.value = _health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            _health -= _damageDealt;
            if(_health <= 0) Die();
        }
    }

    public void Die()
    {
            Destroy(gameObject, 0.5f);
    }
}


/*public interface HEALTH
{ 
    void Damage();
}*/