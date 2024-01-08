using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    public Vector3 vect;

    public float _speed;
    [SerializeField] int _playerHealth;
    [SerializeField] float _dampingForce;
    [SerializeField] float _flickerInterval = 0.1f;
    [SerializeField] float _suicideHeight;
    [SerializeField] float _rotationSpeed;
    [SerializeField] Slider _playerHealthbar;
    [SerializeField] Vector3 force;

    [SerializeField] Rigidbody _rigidbody;
    private Renderer _renderer;
    private void Awake()
    {
        if (!TryGetComponent(out _rigidbody)) Debug.Log("Player:RigidBody:NULL");
        if (!TryGetComponent(out _renderer)) Debug.Log("Player:Renderer:NULL");
    }
    private void Update()
    {
        //Time.timeScale = 1;
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
                Debug.Log(GetComponentInParent<Rigidbody>().velocity + (100 * _speed * Time.fixedDeltaTime * rotatedDirection));
            }
        }
        _rigidbody.velocity = force;
        //_rigidbody.MovePosition(_rigidbody.position + _speed * direction *Time.fixedDeltaTime);
        
        float moveAngle = Mathf.Atan2(rotatedDirection.x, rotatedDirection.z) * Mathf.Rad2Deg; // Calculate the rotation angle for the offset direction.
        if (rotatedDirection != Vector3.zero)
        {
            Quaternion playerRotation = Quaternion.Euler(0, moveAngle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, _rotationSpeed * 100 * Time.fixedDeltaTime);
        }        
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
        if (transform.position.y <= -_suicideHeight)
        {
            UIManager.Instance.FellIntoVoid();
        }
    }
    public void ApplyKnockback(Vector3 knockBackDirection, float knockBackForce, float knockBackDuration)
    {
        if (gameObject.activeSelf)
        {
            _rigidbody.AddForce(knockBackDirection * knockBackForce, ForceMode.Impulse);
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