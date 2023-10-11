using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public FixedJoystick joystick;
    
    [SerializeField] private float _speed;
    [SerializeField] private float _playerHealth;

    [SerializeField] private Slider _playerHealthbar;
    [SerializeField] private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null) Debug.Log("Player:RigidBody:NULL");
    }
    void Update()
    {
        HandleMovement();
        DisplayHealth();

        _playerHealthbar.gameObject.transform.rotation = Quaternion.identity;
    }
    void HandleMovement()
    {
        _rigidbody.AddForce((1000 * _speed) * Time.deltaTime * new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized, ForceMode.Force);

        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            float moveAngle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, moveAngle, 0);
        }
    }
    void DisplayHealth()
    {
        _playerHealthbar.value = _playerHealth;
    }

    public void Damage(float damage)
    {
        _playerHealth -= damage;
    }
}