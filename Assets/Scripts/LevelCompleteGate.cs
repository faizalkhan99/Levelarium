using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteGate : MonoBehaviour
{
    [SerializeField] private AudioClip _levelCompletedClip;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && GameManager.Instance.HasKey && GameManager.Instance.IsSpawnerDead)
        {
            if(PlayerPrefs.GetInt("levels", 0) < SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("levels", SceneManager.GetActiveScene().buildIndex);
            }
            AudioManager.Instance.LevelCompletedSFX(_levelCompletedClip);
            UIManager.Instance.NextLevelPanel();
        }
    }
}
