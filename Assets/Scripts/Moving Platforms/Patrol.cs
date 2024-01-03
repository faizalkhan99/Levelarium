using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] Transform currentWaypoint;

    [SerializeField] int currentWaypointIndex = 0;
    
    [SerializeField] float waypointreaxhedThreshold = 0.1f;
    [SerializeField] float pauseTime = 0.5f;
    [SerializeField] float _moveSpeed;
    private float _timer = 0f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        if(wayPoints.Length > 0) currentWaypoint = wayPoints[currentWaypointIndex];
    }
    void FixedUpdate()
    {
        HandleMovement();   
    }
    void HandleMovement()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            Vector3 targetPosition = new Vector3(wayPoints[currentWaypointIndex].position.x, transform.position.y, wayPoints[currentWaypointIndex].position.z);

            // Calculate the move direction and normalize it
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            // Calculate the new position using MovePosition
            Vector3 newPosition = transform.position + moveDirection * _moveSpeed * Time.deltaTime;

            // Move the Rigidbody to the new position
            _rb.MovePosition(newPosition);
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentWaypoint.position.x, transform.position.y, currentWaypoint.position.z), Time.deltaTime * _moveSpeed);
            if (Vector3.Distance(transform.position, currentWaypoint.position) < waypointreaxhedThreshold)
            {
                _timer = pauseTime;
                SwitchWaypoint();
            }
        }
    }
    void SwitchWaypoint()
    {
        if (currentWaypointIndex == wayPoints.Length - 1)
        {
            currentWaypointIndex = 0;
        }
        else
        {
            currentWaypointIndex++;
        }
        currentWaypoint = wayPoints[currentWaypointIndex];
    }
}