using UnityEngine;

public class MachineTrigger : MonoBehaviour
{

    [SerializeField] private GameObject _machine;
    [SerializeField] EnemySpawner _enemySpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(_enemySpawner.SpanwEnemies());
            gameObject.transform.position = new(transform.position.x, 10f, transform.position.z);
            _enemySpawner.Gate(true);
        }
    }
}
