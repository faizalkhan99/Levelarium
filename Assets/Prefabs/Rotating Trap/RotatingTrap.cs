using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTrap : MonoBehaviour
{
    [SerializeField] private float _xSpeed;
    [SerializeField] private float _ySpeed;
    [SerializeField] private float _zSpeed;

    void Update()
    {
        transform.Rotate(_xSpeed * Time.deltaTime, _ySpeed * Time.deltaTime, _zSpeed * Time.deltaTime);
    }
}