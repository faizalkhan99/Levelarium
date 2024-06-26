using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    public Vector3 vect;

    public float _speed;
    [SerializeField] private int _health;
    [SerializeField] float _dampingForce;
    [SerializeField] float _flickerInterval = 0.1f;
    [SerializeField] float _suicideHeight;
    [SerializeField] float _rotationSpeed;
    [SerializeField] Slider _playerHealthbar;
    [SerializeField] private bool canTakeDamage = true;
    [SerializeField] private float damageCooldown = 1f;
    [SerializeField] Vector3 force;

    AudioSource _movesfx;

    [SerializeField] Rigidbody _rigidbody;
    private Renderer _renderer;

    private void Start()
    {
        _movesfx.volume = 0;
        _movesfx.pitch = 0.5f;
    }
    private void Awake()
    {
        if (!TryGetComponent<AudioSource>(out _movesfx)) Debug.Log("Player:AudioSource:NULL");
        if (!TryGetComponent(out _rigidbody)) Debug.Log("Player:RigidBody:NULL");
        if (!TryGetComponent(out _renderer)) Debug.Log("Player:Renderer:NULL");
    }
    private void Update()
    {
        DisplayHealth();
        _playerHealthbar.gameObject.transform.rotation = Quaternion.identity;
        CheckFall();
    }

    public float acceleration = 10f; // Adjust as needed
    public float deceleration = 10f; // Adjust as needed
    private Vector3 currentVelocity = Vector3.zero;

    void FixedUpdate()
    {
        HandleMovement();
    }
    public bool _isStandingOnMovingPlatform = false;
    void HandleMovement()
    {
        float offsetAngle = -45.0f; // The angle by which you want to offset the movement.
        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        Quaternion rotation = Quaternion.Euler(0, offsetAngle, 0); // Apply the offset angle to the direction vector.
        Vector3 rotatedDirection = rotation * direction;
        force = (100 * _speed) * Time.fixedDeltaTime * rotatedDirection;

        if (transform.parent != null && transform.parent.CompareTag("MovingPlatform"))
        {
            _isStandingOnMovingPlatform = true;
            GetComponent<Collider>().material.dynamicFriction = 99999f;
            if (direction.magnitude > 0.2f)
            {
                GetComponent<Collider>().material.dynamicFriction = 1.0f;
            }
        }

        if (force.magnitude < currentVelocity.magnitude) // If decelerating
        {
            currentVelocity = Vector3.Lerp(currentVelocity, force, Time.deltaTime * deceleration);
        }
        else // If accelerating
        {
            currentVelocity = Vector3.Lerp(currentVelocity, force, Time.deltaTime * acceleration);
        }

        // Apply the velocity to the Rigidbody
        _rigidbody.velocity = currentVelocity;


        //_rigidbody.velocity = force;

        _movesfx.pitch = Mathf.Lerp(_movesfx.pitch, (direction != Vector3.zero) ? 1 : 0.5f, deceleration * Time.deltaTime);
        _movesfx.volume = Mathf.Lerp(_movesfx.volume, (direction != Vector3.zero) ? .5f : 0, deceleration * Time.deltaTime);



        float moveAngle = Mathf.Atan2(rotatedDirection.x, rotatedDirection.z) * Mathf.Rad2Deg; // Calculate the rotation angle for the offset direction.
        if (rotatedDirection != Vector3.zero)
        {
            Quaternion playerRotation = Quaternion.Euler(0, moveAngle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, _rotationSpeed * 100 * Time.fixedDeltaTime);
        }
    }
    void DisplayHealth() => _playerHealthbar.value = _health;
    public void Damage(int damage, Vector3 knockBackDirection, float knockBackForce, float knockBackDuration)
    {
        if (canTakeDamage)
        {
            StartCoroutine(DamageCooldown());
            ApplyKnockback(knockBackDirection, knockBackForce, knockBackDuration);
            _health -= damage;
            if (_health <= 0)
            {
                UIManager.Instance.LevelFailedPanel(true);
                gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
    private void CheckFall()
    {
        if (transform.position.y <= -_suicideHeight)
        {
            UIManager.Instance.FellIntoVoid();
        }
    }
    public void ApplyKnockback(Vector3 knockBackDirection, float knockBackForce, float knockBackDuration)
    {
        if (gameObject.activeSelf)
        {
            Debug.Log("Before: " + _rigidbody.velocity);
            _rigidbody.AddForce(knockBackDirection * knockBackForce, ForceMode.Impulse);
            Debug.Log("After: " + _rigidbody.velocity);
            StartCoroutine(ApplyKnockbackDuration(knockBackDuration));
        }
    }
    private IEnumerator ApplyKnockbackDuration(float knockBackDuration)
    {
        float flickerTimer = 0f;
        while (flickerTimer < knockBackDuration)
        {
            _renderer.enabled = !_renderer.enabled;
            yield return new WaitForSeconds(_flickerInterval);
            flickerTimer += _flickerInterval;
        }
        _renderer.enabled = true;
    }
}