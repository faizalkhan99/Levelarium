using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public FixedJoystick joystick;

    [SerializeField] float _speed;
    [SerializeField] float _playerHealth;
    [SerializeField] float _rotationSpeed;

    [SerializeField] Slider _playerHealthbar;
    
    [SerializeField] Rigidbody _rigidbody;


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
        CheckFall();
    }
    void HandleMovement()
    {
        _rigidbody.AddForce((1000 * _speed) * Time.deltaTime * new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized, ForceMode.Force);
        float moveAngle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Quaternion playerRotation = Quaternion.Euler(0, moveAngle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, (1000 *_rotationSpeed) * Time.deltaTime);
        }
    }
    void DisplayHealth()
    {
        _playerHealthbar.value = _playerHealth;
    }

    public void Damage(float damage)
    {
        _playerHealth -= damage;
        if (_playerHealth <= 0)
        {
            Debug.Log("lmao ded, try again");
            UIManager.Instance.LevelFailedPanel(true);
        }
    }

    private void CheckFall()
    {
        if(transform.position.y <= -30.0f)
        {
            UIManager.Instance.FellIntoVoid();
        }
    }

}