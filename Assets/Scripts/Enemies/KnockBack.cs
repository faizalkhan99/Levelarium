using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Vector3 knockBackDirection = Vector3.forward;
    [SerializeField] private float knockBackDuration;
    [SerializeField] private float knockBackForce;
    [SerializeField] private int _damageGivenToPlayer;
    private Vector3 lastFramePosition;

    private void Update()
    {
        lastFramePosition = transform.position;
        // Calculate the movement direction since the last frame
        Vector3 movementDirection = transform.position - lastFramePosition;
        // Update knockBackDirection based on movement direction
        knockBackDirection = movementDirection.normalized;
        // Save current position for the next frame
        lastFramePosition = transform.position;
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.TryGetComponent<PlayerMovement>(out var _player))
            {
                _player.Damage(_damageGivenToPlayer, knockBackDirection, knockBackForce, knockBackDuration);
                
            }
        }
    }
}
