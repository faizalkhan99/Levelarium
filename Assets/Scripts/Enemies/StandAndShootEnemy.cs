using System.Collections;
using UnityEngine;

public class StandAndShootEnemy : MonoBehaviour
{
    Shoot _shoot;
    private bool _shooting = false;
    private Coroutine _shootCoroutine;

    private void Awake()
    {
        _shoot = GetComponent<Shoot>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.LookAt(other.gameObject.transform);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _shooting = true;
            _shootCoroutine = StartCoroutine(ShootAtPlayer());
        }
    }
    IEnumerator ShootAtPlayer()
    {
        while (_shooting)
        {
            yield return new WaitForSeconds(1.5f);
            if (_shoot) _shoot.FireBullet();
            Debug.Log("fire at player");
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        _shooting = false;
        if(_shootCoroutine != null) StopCoroutine(ShootAtPlayer());
    }
}
