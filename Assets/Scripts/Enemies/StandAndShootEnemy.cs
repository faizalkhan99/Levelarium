using System.Collections;
using UnityEngine;

public class StandAndShootEnemy : MonoBehaviour
{
    Shoot _shoot;
    [SerializeField] private bool _shooting = false;
    private Coroutine _shootCoroutine;

    private void Awake()
    {
        _shoot = GetComponent<Shoot>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.LookAt(other.gameObject.transform);//add rigidbodies and use _rb.RotateTowards(); to enemies who look towards player to avoid snapping issue.
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
            Debug.Log("StandAndShootEnemy:ShootAtPlayer();");
        }
        Debug.Log("StandAndShootEnemy:ShootAtPlayer():Stopped Shooting;");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (_shootCoroutine != null)
            {
                StopCoroutine(ShootAtPlayer());
                _shootCoroutine = null;
            }
            _shooting = false;
        }
    }
}
