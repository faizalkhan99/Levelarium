using UnityEngine;

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

    private void Awake()
    {
        _instance = this;
    }

    private int _levelCounter = 1;
    public void NextLevelPanel()
    {
        Time.timeScale = 0f;
        _nextLevelPanel.SetActive(true);
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", _levelCounter);
        }
        else
        {
            _levelCounter++;
        }
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
        Time.timeScale = 0.4f;
        _fellIntoVoidPanel.SetActive(true);
    }
}
