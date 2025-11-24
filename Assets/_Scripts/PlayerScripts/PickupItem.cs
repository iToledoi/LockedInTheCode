using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// put this script on any object you want to pick up
[RequireComponent(typeof(Rigidbody))]
public class PickupItem : MonoBehaviour
{
    public bool IsHeld { get; private set; }
    Rigidbody rb;
    Transform originalParent, followTarget;
    float moveSpeed = 22f, rotateSpeed = 22f;
    public float maxFollowSpeed = 10f; // clamp to avoid teleporting through colliders

    void Awake(){ rb = GetComponent<Rigidbody>(); originalParent = transform.parent; }

    void FixedUpdate()
    {
        if (!IsHeld || followTarget == null) return;

        // Move physics body toward the target so collisions still occur.
        Vector3 toTarget = followTarget.position - rb.position;

        // Desired velocity proportional to distance (spring-like). Clamp to maxFollowSpeed.
        Vector3 desiredVel = toTarget * moveSpeed;
        if (desiredVel.magnitude > maxFollowSpeed)
            desiredVel = desiredVel.normalized * maxFollowSpeed;

        // Smoothly change velocity (helps avoid popping through thin colliders).
        rb.velocity = Vector3.Lerp(rb.velocity, desiredVel, 0.9f);

        // Smoothly rotate toward the hold rotation using MoveRotation so physics interacts normally.
        Quaternion targetRot = followTarget.rotation;
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, rotateSpeed * Time.fixedDeltaTime));
    }
    public void PickUp(Transform holdPoint, float followMoveSpeed = 20f, float followRotateSpeed = 20f)
    {
        if (IsHeld) return;

        followTarget = holdPoint;
        moveSpeed = followMoveSpeed;
        rotateSpeed = followRotateSpeed;

        // IMPORTANT: zero velocities BEFORE changing state
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Keep the rigidbody non-kinematic so collisions still occur, disable gravity while held
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        IsHeld = true;

        // Do NOT parent the object to the holdPoint. We'll follow it using physics so collisions remain.
    }
    public void Drop(){
        if (!IsHeld) return;
        IsHeld = false;
        followTarget = null;

        // Restore physics behavior
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rb.interpolation = RigidbodyInterpolation.None;

        // Restore parent to original if any
        transform.SetParent(originalParent, true);

        var cam = Camera.main;
        if (cam) rb.AddForce(cam.transform.forward * 0.5f, ForceMode.VelocityChange);
    }
}