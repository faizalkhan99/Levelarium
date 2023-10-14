using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && GameManager.Instance.HasKey && GameManager.Instance.IsSpawnerDead)
        {
            if(PlayerPrefs.GetInt("levels", 0) < SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("levels", SceneManager.GetActiveScene().buildIndex);
            }
            UIManager.Instance.NextLevelPanel();
        }
    }
}
