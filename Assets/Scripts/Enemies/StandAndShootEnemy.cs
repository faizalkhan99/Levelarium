using System.Collections;
using UnityEngine;

public class StandAndShootEnemy : MonoBehaviour
{
    
    Shoot _shoot;
    [SerializeField] private bool _shooting = false;
    [SerializeField] private float _rotationSpeed;
    private Coroutine _shootCoroutine;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _shoot = GetComponent<Shoot>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Determine the rotation towards the target
            Quaternion targetRotation = Quaternion.LookRotation(other.transform.position - transform.position);
            // Smoothly rotate towards the target using RotateTowards
            Quaternion newRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            // Apply the new rotation to the Rigidbody
            rb.MoveRotation(newRotation);
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
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
            yield return new WaitForSeconds(2.0f);
            if (_shoot) _shoot.FireBullet();
        }
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
    private void Start()
    {

    }

}
