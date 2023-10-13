using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] Slider _progressBar;
    [SerializeField] GameObject _loadingScreen;

    private void Awake()
    {
        if (_progressBar == null || _loadingScreen == null) Debug.Log("Something was NULL amongst Proress Bar, Level Selector Panel or Loading Screen");
    }
    public void LoadScene(string sceneName)
    {
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadingAsync(sceneName));
    }
    IEnumerator LoadingAsync(string sceneName)
    {
        Time.timeScale = 1.0f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            _progressBar.value = operation.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}