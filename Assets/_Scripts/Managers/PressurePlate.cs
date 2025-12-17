using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    //[SerializeField] private PressurePlateDoor currentDoor;
    ////[SerializeField] private Animator animator;

    //private void OnTriggerStay(Collider other)
    //{
    //    //if (other.CompareTag("Player"))
    //    //{
    //        currentDoor.addPressurePlate(this);
    //        //animator.SetBool("isPressed", true);
    //    //}
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    //if (other.CompareTag("Player"))
    //    //{
    //        currentDoor.removePressurePlate(this);
    //        //animator.SetBool("isPressed", false);
    //    //}
    //}

    [Header("Pressure Plate Parts")]
    public Transform plateTop;

    [Header("Movement Settings")]
    public float pressedHeight = - 0.1f;
    public float moveSpeed = 3f;
    public float requiredWeight = 10f;
    private float currentWeight = 0f;
    private Vector3 initialPosition;
    public LockedDoor door;

    private HashSet<Rigidbody> objectOnPlate = new HashSet<Rigidbody>();
    private void Start()
    {
        initialPosition = plateTop.localPosition;
    }

    private void Update()
    {
        Vector3 targetPosition = initialPosition;
        if (currentWeight > 0)
        {
            targetPosition = initialPosition + Vector3.up * pressedHeight;
        }
        plateTop.localPosition = Vector3.Lerp(plateTop.localPosition, targetPosition, Time.deltaTime * moveSpeed);

        if (currentWeight >= requiredWeight)
        {
            ActivatePlate();
        }
        else
        {
            DeactivatePlate();
        }
        Debug.Log("Current Weight on Plate: " + currentWeight);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered pressure plate: " + other.name);
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && !objectOnPlate.Contains(rb))
        {
            objectOnPlate.Add(rb);
            currentWeight += rb.mass;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && objectOnPlate.Contains(rb))
        {
            objectOnPlate.Remove(rb);
            currentWeight -= rb.mass;
        }
    }

    private void ActivatePlate()
    {
        // Logic to activate the pressure plate
        // e.g., open a door, trigger an event, etc.
        //door.OpenDoor();
        Destroy(gameObject);
        Debug.Log("Door opened by pressure plate.");
    }
    private void DeactivatePlate()
    {
        // Logic to deactivate the pressure plate
        // e.g., close a door, reset an event, etc.
    }
}
