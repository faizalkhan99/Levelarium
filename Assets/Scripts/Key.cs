using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.IsSpawnerDead)
        {
            GameManager.Instance.HasKey = true;
            GameManager.Instance.InstantiateGate();
            AudioManager.Instance.KeyObtainedSFX(_clip);
            Destroy(gameObject);
        }
    }
}
