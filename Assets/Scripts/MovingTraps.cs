using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTraps : MonoBehaviour
{
    private Vector3 knockBackDirection = Vector3.back;
    [SerializeField] private float knockBackDuration;
    [SerializeField] private float knockBackForce;
    [SerializeField] private int _damageGivenToPlayer;
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerMovement _player = other.gameObject.GetComponent<PlayerMovement>();
            if(_player != null)
            {
                _player.ApplyKnockback(knockBackDirection, knockBackForce, knockBackDuration);
                _player.Damage(_damageGivenToPlayer);
            }
        }
    }
}
