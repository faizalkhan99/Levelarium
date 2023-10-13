using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] string _levelnamme;
    LoadingScreen _loadingScreen;
    private void Awake()
    {
        _loadingScreen = GameObject.Find("UI MANAGER").GetComponent<LoadingScreen>();
        if (_loadingScreen == null) Debug.Log("UI MANAGER was NULL");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _loadingScreen.LoadScene(_levelnamme);
        }
    }
}
