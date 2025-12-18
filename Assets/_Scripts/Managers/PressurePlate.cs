using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Pressure Plate Parts")]
    public Transform plateTop;

    [Header("Movement Settings")]
    public float pressedHeight = -0.1f;
    public float moveSpeed = 3f;
    public float requiredWeight = 10f;

    float currentWeight = 0f;
    Vector3 initialPosition;

    [Header("Door To Open")]
    public LockedDoor door;          // drag your bars door (with LockedDoor + Animator) here

    HashSet<Rigidbody> objectsOnPlate = new HashSet<Rigidbody>();

    Animator doorAnimator;
    bool doorIsOpen = false;         // track door state ourselves

    void Start()
    {
        initialPosition = plateTop.localPosition;

        if (door != null)
            doorAnimator = door.GetComponent<Animator>();
    }

    void Update()
    {
        // 1) Move plate visual
        Vector3 targetPosition = initialPosition;

        if (currentWeight > 0)
            targetPosition = initialPosition + Vector3.up * pressedHeight;

        plateTop.localPosition = Vector3.Lerp(
            plateTop.localPosition,
            targetPosition,
            Time.deltaTime * moveSpeed
        );

        // 2) Decide if door should be open or closed
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
        if (rb != null && !objectsOnPlate.Contains(rb))
        {
            objectsOnPlate.Add(rb);
            currentWeight += rb.mass;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && objectsOnPlate.Contains(rb))
        {
            objectsOnPlate.Remove(rb);
            currentWeight -= rb.mass;
        }
    }

    void OpenDoor()
    {
        doorIsOpen = true;

        if (doorAnimator != null)
        {
            Debug.Log("Plate → OPEN trigger");
            doorAnimator.ResetTrigger("Close");
            doorAnimator.SetTrigger("Open");
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
