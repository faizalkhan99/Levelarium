using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damageGivenToPlayer;
    Rigidbody rb;
    [SerializeField] private AudioClip _collisionSFX;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<PlayerMovement>(out var _player))
            {
                _player.Damage(damageGivenToPlayer, Vector3.forward, 150f, 2.0f);
                _player.ApplyKnockback(rb.velocity.normalized, 15.0f, 4.0f);
            }
        }
        AudioManager.Instance.PlaySFX(_collisionSFX);
        Destroy(gameObject);
    }
}