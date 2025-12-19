using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Pressure Plate Parts")]
    public Transform plateTop; // the part of the plate that physically moves up/down

    [Header("Movement Settings")]
    public float pressedHeight = -0.1f; // how far down the plate moves when pressed
    public float moveSpeed = 3f;
    public float requiredWeight = 10f; // minimum weight the pressure plate needs to activate

    float currentWeight = 0f;
    Vector3 initialPosition;

    [Header("Door To Open")]
    public LockedDoor door;    // the door that this pressure plate controls   

    HashSet<Rigidbody> objectsOnPlate = new HashSet<Rigidbody>(); // tracking objects on the plate

    Animator doorAnimator;
    bool doorIsOpen = false;       

    void Start()
    {
        initialPosition = plateTop.localPosition;

        if (door != null)
            doorAnimator = door.GetComponent<Animator>(); // get the door's animator
    }

    void Update()
    {

        Vector3 targetPosition = initialPosition; // start with unpressed position

        if (currentWeight > 0)
            targetPosition = initialPosition + Vector3.up * pressedHeight;

        plateTop.localPosition = Vector3.Lerp(plateTop.localPosition, targetPosition, Time.deltaTime * moveSpeed);

        // condition to determine if the pressure plate should open the door
        bool shouldOpen = currentWeight >= requiredWeight;

        if (shouldOpen && !doorIsOpen)
        {
            OpenDoor();
        }
        else if (!shouldOpen && doorIsOpen)
        {
            CloseDoor();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && !objectsOnPlate.Contains(rb)) // ensure it's a rigidbody and not already counted
        {
            objectsOnPlate.Add(rb);
            currentWeight += rb.mass; // adds the object's mass to the current weight tracker
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && objectsOnPlate.Contains(rb))
        {
            objectsOnPlate.Remove(rb);
            currentWeight -= rb.mass; // subtracts the object's mass from the current weight tracker
        }
    }

    void OpenDoor()
    {
        doorIsOpen = true;

        if (doorAnimator != null)
        {
            Debug.Log("Plate → OPEN trigger");
            doorAnimator.ResetTrigger("Close");
            doorAnimator.SetTrigger("Open"); // animations
        }
    }

    void CloseDoor()
    {
        doorIsOpen = false;

        if (doorAnimator != null)
        {
            Debug.Log("Plate → CLOSE trigger");
            doorAnimator.ResetTrigger("Open");
            doorAnimator.SetTrigger("Close");
        }
    }
}
