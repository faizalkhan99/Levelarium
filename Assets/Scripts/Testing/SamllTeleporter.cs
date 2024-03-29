using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamllTeleporter : MonoBehaviour
{

    [Tooltip("Tags of objects that can pass through this teleporter")]
    [SerializeField] private string[] _tags;
    [SerializeField] private Transform _destination;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tags[0]) || other.CompareTag(_tags[1]) || other.CompareTag(_tags[2]))
        {
            other.transform.position = _destination.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
