using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform GetWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    // Gets the next waypoint index
    public int GetNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1; // increment index to get next waypoint
        if (nextWaypointIndex >= transform.childCount) // if index exceeds number of waypoints, loop back to start
        {
            nextWaypointIndex = 0;
        }
        return nextWaypointIndex;
    }

    // Gets the previous waypoint index
    public int GetPreviousWaypointIndex(int currentWaypointIndex)
    {
        int previousWaypointIndex = currentWaypointIndex - 1; // decrement index to get previous waypoint
        if (previousWaypointIndex < 0)
        {
            previousWaypointIndex = transform.childCount - 1; // loop back to last waypoint if index is negative
        }
        return previousWaypointIndex;
    }

}
