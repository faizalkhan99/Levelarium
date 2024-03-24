using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private Transform _spawnPosition;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerSpawn:OnTriggerEnter();");
        if(other.CompareTag("Player"))
        Instantiate(_enemyToSpawn, _spawnPosition.position, Quaternion.identity);
    }
}
