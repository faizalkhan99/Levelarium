using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeStabalizer : MonoBehaviour
{
    private float playerOriginalSpeed;
    [SerializeField] Transform playerTransform;
    PlayerMovement player;
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    void Start()
    {
        playerOriginalSpeed = player._speed; //original speed = 2
    }
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
            player._speed = playerOriginalSpeed; // new speed = original speed => 2
        }
    }
}