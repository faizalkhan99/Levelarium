using System.Collections;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] Transform currentWaypoint;

    [SerializeField] int currentWaypointIndex = 0;
    
    [SerializeField] float waypointreaxhedThreshold = 0.1f;
    public float pauseTime = 0.5f;
    [SerializeField] float _moveSpeed;
    private float _timer = 0f;
    [SerializeField] private bool _isMoving = true;

    [SerializeField] private GameObject _boundary;

    private Rigidbody _rb;

    private void Awake()
    {
       if(_boundary) _boundary.SetActive(false);
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
            Vector3 targetPosition = new(wayPoints[currentWaypointIndex].position.x, transform.position.y, wayPoints[currentWaypointIndex].position.z);

            // Calculate the move direction and normalize it
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            // Calculate the new position using MovePosition
            Vector3 newPosition = transform.position + _moveSpeed * Time.fixedDeltaTime * moveDirection;

            // Move the Rigidbody to the new position
            _rb.MovePosition(newPosition);
            if (Vector3.Distance(transform.position, currentWaypoint.position) < waypointreaxhedThreshold)
            {
                if (_boundary) _boundary.SetActive(false); //boundary invisible   
                _timer = pauseTime;
                SwitchWaypoint();
            }
            else
            {
                if (_boundary) _boundary.SetActive(true); //boundary visible
            }
        }
    }
    private IEnumerator Pause(bool condition)
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("waited");
        _isMoving = false;
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