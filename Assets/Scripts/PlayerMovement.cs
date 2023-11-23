using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;

    [SerializeField] public float _speed;
    [SerializeField] int _playerHealth;
    [SerializeField] float _dampingForce;
    [SerializeField] private float flickerInterval = 0.1f;
    [SerializeField] Slider _playerHealthbar;

    [SerializeField] Rigidbody _rigidbody;
    private Renderer _renderer;
    private void Awake()
    {
        if (!TryGetComponent<Rigidbody>(out _rigidbody)) Debug.Log("Player:RigidBody:NULL");
        if (!TryGetComponent<Renderer>(out _renderer)) Debug.Log("Player:Renderer:NULL");
    }
    private void Update()
    {
        DisplayHealth();
        _playerHealthbar.gameObject.transform.rotation = Quaternion.identity;
        CheckFall();
    }
    void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ramp"))
        {
            _rigidbody.useGravity = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ramp"))
        {
            _rigidbody.useGravity = true;
        }
    }
    void HandleMovement()
    {
        float offsetAngle = -45.0f; // The angle by which you want to offset the movement.
        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
        Quaternion rotation = Quaternion.Euler(0, offsetAngle, 0); // Apply the offset angle to the direction vector.
        Vector3 rotatedDirection = rotation * direction;
        Vector3 force = (1000 * _speed) * Time.deltaTime * rotatedDirection;
        if (direction == Vector3.zero)
        {
            force += -_rigidbody.velocity.normalized * _dampingForce;
        }
        _rigidbody.AddForce(force, ForceMode.Force);
        float moveAngle = Mathf.Atan2(rotatedDirection.x, rotatedDirection.z) * Mathf.Rad2Deg; // Calculate the rotation angle for the offset direction.

        if (rotatedDirection != Vector3.zero)
        {
            Quaternion playerRotation = Quaternion.Euler(0, moveAngle, 0);
            _rigidbody.MoveRotation(playerRotation); // Rotate the player towards the offset direction.
        }

        /*Vector3 rotateDirection = Vector3.RotateTowards(transform.forward, new Vector3(joystick.Horizontal, 0f, joystick.Vertical), rotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(rotateDirection);*/
    }
    void DisplayHealth() => _playerHealthbar.value = _playerHealth;
    public void Damage(int damage)
    {
        _playerHealth -= damage;
        if (_playerHealth <= 0)
        {
            UIManager.Instance.LevelFailedPanel(true);
            gameObject.SetActive(false);
        }
    }
    private void CheckFall()
    {
        if (transform.position.y <= -10.0f)
        {
            UIManager.Instance.FellIntoVoid();
        }
    }
    public void ApplyKnockback(Vector3 knockBackDirection, float knockBackForce, float knockBackDuration)
    {
        _rigidbody.AddForce(knockBackDirection * knockBackForce, ForceMode.Impulse);
        StartCoroutine(ApplyKnockbackDuration(knockBackDuration));
    }
    private IEnumerator ApplyKnockbackDuration(float knockBackDuration)
    {
        float flickerTimer = 0f;

        while (flickerTimer < knockBackDuration)
        {
            _renderer.enabled = !_renderer.enabled;
            yield return new WaitForSeconds(flickerInterval);
            flickerTimer += flickerInterval;
        }

        _renderer.enabled = true;
    }

}