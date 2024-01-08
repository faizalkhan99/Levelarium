using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blender_test : MonoBehaviour
{
  
    void Update()
    {
        transform.position += Vector3.forward * 10 * Time.deltaTime; 
    }
}
