using UnityEngine;

public class MachineTrigger : MonoBehaviour
{

    [SerializeField] private GameObject _machine;
    [SerializeField] EnemySpawner _enemySpawner;
    private void Awake()
    {
        _machine.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.transform.position = new(transform.position.x, 10f, transform.position.z);
            _machine.SetActive(true);
            _enemySpawner.Gate(true);
        }
    }
}
