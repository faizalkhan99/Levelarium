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
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, Time.deltaTime * _moveSpeed);
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
