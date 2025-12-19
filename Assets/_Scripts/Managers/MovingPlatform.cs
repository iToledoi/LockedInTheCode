using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private WaypointPath waypointPath; // waypointPath holds empty game objects outlining the path the platform will follow

    [SerializeField]
    private float speed = 2f; // default speed of the platform, should be adjusted to increase difficulty of parkour
    private int currentWaypointIndex = 0;
    private int targetWaypointIndex = 0;
    [SerializeField]
    private bool isMovingAutomatically = false; // initially set to false so platform only moves when triggered
    private Transform player;
    private Vector3 lastPosition;

    // initialize the platform
    private void Start()
    {
        targetWaypointIndex = currentWaypointIndex;
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // move the platform towards the target waypoint
        if (currentWaypointIndex != targetWaypointIndex || isMovingAutomatically)
        {
            MoveTowardsTarget();
        }

        // move the player along with the platform
        Vector3 delta = transform.position - lastPosition;
        if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.MovePosition(rb.position + delta);
            }
        }
        lastPosition = transform.position;
    }

    // moves the platform towards the target waypoint
    private void MoveTowardsTarget()
    {
        Transform target = waypointPath.GetWaypoint(targetWaypointIndex);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime); // moves the platform towards the target waypoint at the specified speed
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
            // checks if the object is close enough to the target waypoint
        {
            currentWaypointIndex = targetWaypointIndex;
            if (isMovingAutomatically)
            {
                targetWaypointIndex = waypointPath.GetNextWaypointIndex(currentWaypointIndex);
            }
        }
    }

    //  methods to control platfom direction along waypoints and stop/start movement
    public void MoveToNextWaypoint()
    {
        isMovingAutomatically = false;
        targetWaypointIndex = waypointPath.GetNextWaypointIndex(currentWaypointIndex);
    }

    public void MoveToPreviousWaypoint()
    {
        isMovingAutomatically = false;
        targetWaypointIndex = waypointPath.GetPreviousWaypointIndex(currentWaypointIndex);
    }

    // starts/stops automatic movement through waypoints
    public void StartAutomaticMovement()
    {
        isMovingAutomatically = true;
        targetWaypointIndex = waypointPath.GetNextWaypointIndex(currentWaypointIndex);
    }

    public void StopAutomaticMovement()
    {
        isMovingAutomatically = false; // platform will stop at the current waypoint
    }

    // detects if player is within the platform collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            player = collision.transform;
        }
    }

    // detects if player exits the platform collider
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
}
