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
    [SerializeField] GameObject _pauseButton; 
    [SerializeField] GameObject _levelfailedPanel;
    [SerializeField] GameObject _fellIntoVoidPanel;
    [SerializeField] GameObject _creditsPanel;
    [SerializeField] GameObject _mainmenuPanel;
    [SerializeField] GameObject _resetPanel;

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
        Time.timeScale = 1.0f;
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadingAsync(sceneName));
    }
    IEnumerator LoadingAsync(string sceneName)
    {
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
        AudioManager.Instance.PauseBGM();
        _nextLevelPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void LevelFailedPanel(bool condition)
    {
        AudioManager.Instance.PauseBGM();
        _levelfailedPanel.SetActive(condition);
        Time.timeScale = 0f;
    }

    public void PauseUnpauseGame(bool condition)
    {
        if (condition)
        {
            AudioManager.Instance.PauseBGM();
            _pauseButton.SetActive(!condition);
            _pauseMenuPanel.SetActive(condition);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            _pauseButton.SetActive(!condition);
            _pauseMenuPanel.SetActive(condition);
            AudioManager.Instance.PlayBGM();
        }
    }

    public void FellIntoVoid()
    {
        AudioManager.Instance.PauseBGM();
        if (_fellIntoVoidPanel != null) _fellIntoVoidPanel.SetActive(true);
    }

    public void TurnOFFEverything()
    {
        if(_nextLevelPanel != null) _nextLevelPanel.SetActive(false);
        if (_pauseMenuPanel != null) _pauseMenuPanel.SetActive(false);
        if (_pauseButton != null) _pauseButton.SetActive(false);
        if (_levelfailedPanel != null) _levelfailedPanel.SetActive(false);
        if (_fellIntoVoidPanel != null) _fellIntoVoidPanel.SetActive(false);
        if (_mainmenuPanel != null) _mainmenuPanel.SetActive(false);
        if (_creditsPanel != null) _creditsPanel.SetActive(false);
        if (_resetPanel != null) _resetPanel.SetActive(false);

    }
    public void Credits()
    {
        TurnOFFEverything();
        _creditsPanel.SetActive(true); 
    }
    public void ToMainMenu()
    {
        TurnOFFEverything();
        _mainmenuPanel.SetActive(true);
    }

    public void ResetPanel()
    {
        TurnOFFEverything();
        _resetPanel.SetActive(true);
    }
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        ToMainMenu();
    }
    public void Quit() => Application.Quit();
}
