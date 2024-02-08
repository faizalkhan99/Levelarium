using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTrap : MonoBehaviour
{
    [SerializeField] private float _rotatingSpeesd;

    void Update()
    {
        transform.Rotate(0, 0, _rotatingSpeesd);
    }
}
