using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private WaypointPath waypointPath;

    [SerializeField]
    private float speed = 2f;
    private int currentWaypointIndex = 0;
    private int targetWaypointIndex = 0;
    [SerializeField]
    private bool isMovingAutomatically = false;
    private Transform player;
    private Vector3 lastPosition;

    // Initialize the platform
    private void Start()
    {
        targetWaypointIndex = currentWaypointIndex;
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Move the platform towards the target waypoint
        if (currentWaypointIndex != targetWaypointIndex || isMovingAutomatically)
        {
            MoveTowardsTarget();
        }

        // Move the player along with the platform
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

    // Moves the platform towards the target waypoint
    private void MoveTowardsTarget()
    {
        Transform target = waypointPath.GetWaypoint(targetWaypointIndex);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex = targetWaypointIndex;
            if (isMovingAutomatically)
            {
                targetWaypointIndex = waypointPath.GetNextWaypointIndex(currentWaypointIndex);
            }
        }
    }

    // Public methods to control platform movement  
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

    // Starts/stops automatic movement through waypoints
    public void StartAutomaticMovement()
    {
        isMovingAutomatically = true;
        targetWaypointIndex = waypointPath.GetNextWaypointIndex(currentWaypointIndex);
    }

    public void StopAutomaticMovement()
    {
        isMovingAutomatically = false;
    }

    // Detect player collision to parent/unparent the player to/from the platform
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
}
