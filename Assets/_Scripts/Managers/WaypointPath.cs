using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform GetWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    // Gets the next waypoint index, wrapping around if necessary
    public int GetNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1;
        if (nextWaypointIndex >= transform.childCount)
        {
            nextWaypointIndex = 0;
        }
        return nextWaypointIndex;
    }

    // Gets the previous waypoint index, wrapping around if necessary
    public int GetPreviousWaypointIndex(int currentWaypointIndex)
    {
        int previousWaypointIndex = currentWaypointIndex - 1;
        if (previousWaypointIndex < 0)
        {
            previousWaypointIndex = transform.childCount - 1;
        }
        return previousWaypointIndex;
    }

}
