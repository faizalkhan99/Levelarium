using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    [SerializeField] private int damageGivenToPlayer;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<PlayerMovement>(out var _player))
            {
                Debug.Log("log");
                _player.Damage(damageGivenToPlayer);
                _player.ApplyKnockback(rb.velocity.normalized, 15.0f, 4.0f);
            }
            Destroy(gameObject);
        }
    }

    
}
