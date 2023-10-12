using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.IsSpawnerDead)
        {
            GameManager.Instance.HasKey = true;
            GameManager.Instance.InstantiateGate();
            Destroy(gameObject);
        }
    }
}
