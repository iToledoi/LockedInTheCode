using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateDoor : MonoBehaviour
{
    public bool isDoorOpen = false; // all doors initially are closed
    public float destroyDelay = 0f;
    [SerializeField] private int requiredSwitchesToOpen = 1;
    // change this to set how many pressure plates are needed to open the door

    private List<PressurePlate> currentSwitchesOpen = new();

    public void addPressurePlate(PressurePlate currentPlate) // add a pressure plate to the list of currently activated plates
    {
        if (!currentSwitchesOpen.Contains(currentPlate))
        {
            currentSwitchesOpen.Add(currentPlate);
        }
        TryOpen();
    }

    public void removePressurePlate(PressurePlate currentPlate) // remove a pressure plate from the list of currently activated plates
    {
        if (currentSwitchesOpen.Contains(currentPlate))
        {
            currentSwitchesOpen.Remove(currentPlate);
        }
        TryOpen();
    }

    private void TryOpen() // determine if door should open or close by comparing activated plates to door requirement
    {
        if (currentSwitchesOpen.Count >= requiredSwitchesToOpen)
        {
            openDoor();
        }
        else if (currentSwitchesOpen.Count < requiredSwitchesToOpen)
        {
            closeDoor();
        }
    }

    private void closeDoor()
    {
        isDoorOpen = false;
    }

    private void openDoor() // if door should open, set isDoorOpen to true and destroy the door object
    {
        isDoorOpen = true;
        if (destroyDelay <= 0f)
            Destroy(gameObject);
        else
            Destroy(gameObject, destroyDelay);
    }


}
