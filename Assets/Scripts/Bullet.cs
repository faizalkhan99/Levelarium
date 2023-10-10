using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    EnemySpawner spawner;
    EnemyChasePlayer enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy.DamageEnemy(0.4f);
        }
        else if (other.CompareTag("Spawner"))
        {
            spawner.DecreaseHealth(0.04f);
            //play damage sound here
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject,0.1f);
        }
    }
}
