using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] string _levelToLoad;

    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.LevelButtons(true, _levelToLoad);
    }
    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.LevelButtons(false, "");
    }

}
