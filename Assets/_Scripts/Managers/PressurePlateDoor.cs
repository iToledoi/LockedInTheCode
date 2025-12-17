using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateDoor : MonoBehaviour
{
    public bool isDoorOpen = false;
    public float destroyDelay = 0f;
    [SerializeField] private int requiredSwitchesToOpen = 1;

    private List<PressurePlate> currentSwitchesOpen = new();

    //public Animator doorAnimator;

    public void addPressurePlate(PressurePlate currentPlate)
    {
        if (!currentSwitchesOpen.Contains(currentPlate))
        {
            currentSwitchesOpen.Add(currentPlate);
        }
        TryOpen();
    }

    public void removePressurePlate(PressurePlate currentPlate)
    {
        if (currentSwitchesOpen.Contains(currentPlate))
        {
            currentSwitchesOpen.Remove(currentPlate);
        }
        TryOpen();
    }

    private void TryOpen()
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
        //doorAnimator.SetBool("isOpen", false);
    }

    private void openDoor()
    {
        isDoorOpen = true;
        //doorAnimator.SetBool("isOpen", true);
        if (destroyDelay <= 0f)
            Destroy(gameObject);
        else
            Destroy(gameObject, destroyDelay);
    }


}
