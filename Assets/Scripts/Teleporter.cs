using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform _destination;
    
    [SerializeField] private float _scaleMultiplier;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = _destination.position;
            StartCoroutine(Wobble());
        }
    }

    private IEnumerator Wobble()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * _scaleMultiplier;
        yield return new WaitForSeconds(0.2f);
        transform.localScale = originalScale;
        yield return new WaitForSeconds(0.2f);
        StopCoroutine(Wobble());
    }
}
