using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeStabalizer : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform.parent = transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform.parent = null;
        }
    }
}