using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    GameObject[] _levels;
    private void Awake()
    {
        _levels = GameObject.FindGameObjectsWithTag("locked_levels");

    }
    void Start()
    {
        for(int i = 0; i < _levels.Length; i++)
        {
            if( i < PlayerPrefs.GetInt("levels", 0))
            {
                _levels[i].SetActive(false);
            }
            else
            {
                _levels[i].SetActive(true);
            }
        }
    }


}
