using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPos;
    [SerializeField] private float _bulletForce;
    public void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _shootPos.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.AddForce(_shootPos.forward * (_bulletForce * 1000) * Time.deltaTime, ForceMode.Impulse);
        }
    }
}