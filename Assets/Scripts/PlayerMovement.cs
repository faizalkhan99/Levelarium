using UnityEngine;
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
        if (!TryGetComponent<Rigidbody>(out _rigidbody)) Debug.Log("Player:RigidBody:NULL");
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

        float offsetAngle = -45.0f; // The angle by which you want to offset the movement.

        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        // Apply the offset angle to the direction vector.
        Quaternion rotation = Quaternion.Euler(0, offsetAngle, 0);
        Vector3 rotatedDirection = rotation * direction;

        // Apply force to the rigidbody with the offset direction.
        _rigidbody.AddForce((1000 * _speed) * Time.deltaTime * rotatedDirection, ForceMode.Force);

        // Calculate the rotation angle for the offset direction.
        float moveAngle = Mathf.Atan2(rotatedDirection.x, rotatedDirection.z) * Mathf.Rad2Deg;

        if (rotatedDirection != Vector3.zero)
        {
            // Rotate the player towards the offset direction.
            Quaternion playerRotation = Quaternion.Euler(0, moveAngle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, (1000 * _rotationSpeed) * Time.deltaTime);
        }




        /* _rigidbody.AddForce((1000 * _speed) * Time.deltaTime * new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized, ForceMode.Force);
         float moveAngle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
         if (joystick.Horizontal != 0 || joystick.Vertical != 0)
         {
             Quaternion playerRotation = Quaternion.Euler(0, moveAngle, 0);
             transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, (1000 *_rotationSpeed) * Time.deltaTime);
         }*/
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
            UIManager.Instance.LevelFailedPanel(true);
            Destroy(gameObject);
        }
    }

    private void CheckFall()
    {
        if(transform.position.y <= -20.0f)
        {
            UIManager.Instance.FellIntoVoid();
        }
    }

}