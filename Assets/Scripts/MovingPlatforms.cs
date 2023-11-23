using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] Transform currentWaypoint;
    [SerializeField] Transform playerTransform;

    [SerializeField] int currentWaypointIndex = 0;
    
    [SerializeField] float waypointreaxhedThreshold = 0.1f;
    [SerializeField] float pauseTime = 0.5f;
    [SerializeField] float _moveSpeed;
    private float _timer = 0f;
    private float playerOriginalSpeed;

    PlayerMovement player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    void Start()
    {
        playerOriginalSpeed = player._speed; //original speed = 2
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

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //player._speed += _moveSpeed; //new speed = original speed + platform speed => new speed = 2+5 => 7
            playerTransform.parent = transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform.parent = null;
            player._speed = playerOriginalSpeed; // new speed = original speed => 2
        }
    }
}
