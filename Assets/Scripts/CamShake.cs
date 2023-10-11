using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    private bool _showShake = false;
    [SerializeField] private float _duration = 0.3f; // Adjust the duration as needed
    [SerializeField] private AnimationCurve _curve; // You should have this defined elsewhere.
    private Vector3 originalPosition; // Store the original camera position.

    private void Update()
    {
        if (_showShake)
        {
            _showShake = false;
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        originalPosition = transform.position; // Store the original position.
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = _curve.Evaluate(elapsedTime / _duration);
            Vector3 shakeOffset = Random.insideUnitSphere * strength;
            transform.position = originalPosition + shakeOffset;
            yield return null;
        }

        transform.position = originalPosition; // Reset the camera to its original position.
    }

    public void EnableShake()
    {
        _showShake = true;
    }
}
