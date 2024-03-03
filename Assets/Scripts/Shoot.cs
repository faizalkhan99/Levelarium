using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform[] _shootPos;
    [SerializeField] private float _bulletForce;

    [SerializeField] private AudioClip _shootSFX;
    public void FireBullet()
    {
        foreach (Transform shootPos in _shootPos)
        {
            GameObject bullet = Instantiate(_bulletPrefab, shootPos.position, Quaternion.identity);
            AudioManager.Instance.PlaySFX(_shootSFX);
            if (bullet.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddForce(_bulletForce * (shootPos.forward), ForceMode.Impulse);
            }
            Destroy(bullet, 5.0f);
        }
    }
}