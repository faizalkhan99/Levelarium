using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPos;
    [SerializeField] private float _bulletForce;
    public void FireBullet()
    {
        Debug.Log("Shoot");
        GameObject bullet = Instantiate(_bulletPrefab, _shootPos.position, Quaternion.identity);
        if(bullet.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce((_bulletForce * 1000) * Time.deltaTime * (_shootPos.forward), ForceMode.Impulse);
        }
    }
}