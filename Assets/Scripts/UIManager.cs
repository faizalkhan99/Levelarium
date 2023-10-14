using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("UI MANAGER WAS NULL");
            }
            return _instance;
        }
    }

    [SerializeField] GameObject _nextLevelPanel;
    [SerializeField] GameObject _pauseMenuPanel;
    [SerializeField] GameObject _levelfailedPanel;
    [SerializeField] GameObject _fellIntoVoidPanel;

    [SerializeField] GameObject _levelButton;

    [SerializeField] Slider _progressBar;
    [SerializeField] GameObject _loadingScreen;

    string _levelindex;

    private void Awake()
    {
        _instance = this;
        if (_progressBar == null || _loadingScreen == null || _nextLevelPanel == null || _pauseMenuPanel == null || _levelfailedPanel == null) Debug.Log("Something was NULL from UIManager");
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

    private void Start()
    {
        LevelButtons(false, "");
    }

    public void PlayOnButtonClick()
    {
        LoadScene(_levelindex);
    }
    public void LevelButtons(bool condition, string x)
    {
        _levelindex = x;
        if(_levelButton) _levelButton.SetActive(condition);
    }
    public void NextLevelPanel()
    {
        Time.timeScale = 0f;
        _nextLevelPanel.SetActive(true);
    }
    public void LevelFailedPanel(bool condition)
    {
        Time.timeScale = 0f;
        _levelfailedPanel.SetActive(condition);
    }

    public void PauseUnpauseGame(bool condition)
    {
        if (condition)
        {
            Time.timeScale = 0f;
            _pauseMenuPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            _pauseMenuPanel.SetActive(false);
        }
    }

    public void FellIntoVoid()
    {
        _fellIntoVoidPanel.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
