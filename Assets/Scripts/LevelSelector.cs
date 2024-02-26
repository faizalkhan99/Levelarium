using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    GameObject[] _levels;
    [SerializeField] private string _tagToLookFor;
    private void Awake()
    {
        _levels = GameObject.FindGameObjectsWithTag(_tagToLookFor);

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
