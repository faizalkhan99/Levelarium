using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LevelCompleteGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && GameManager.Instance.HasKey && GameManager.Instance.IsSpawnerDead)
        {
            UIManager.Instance.NextLevelPanel();
        }
    }
}
