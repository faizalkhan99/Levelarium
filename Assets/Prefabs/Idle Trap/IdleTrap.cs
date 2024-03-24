using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTrap : MonoBehaviour
{
    [SerializeField] private GameObject _toOnOff;
    [SerializeField] private float _waitAfterON;
    [SerializeField] private float _waitAfterOFF;
    void Start()
    {
        StartCoroutine(ONOFF());
    }

    private IEnumerator ONOFF()
    {
        while (true)
        {
            _toOnOff.SetActive(true);
            yield return new WaitForSeconds(_waitAfterON);
            _toOnOff.SetActive(false);
            yield return new WaitForSeconds(_waitAfterOFF);
        }
        
    }
}
