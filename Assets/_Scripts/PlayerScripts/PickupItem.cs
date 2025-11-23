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

    void Awake(){ rb = GetComponent<Rigidbody>(); originalParent = transform.parent; }
    void Update(){
        if (!IsHeld || !followTarget) return;
        transform.position = Vector3.Lerp(transform.position, followTarget.position, Time.deltaTime * moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, followTarget.rotation, Time.deltaTime * rotateSpeed);
    }
    public void PickUp(Transform holdPoint, float followMoveSpeed = 20f, float followRotateSpeed = 20f)
{
    if (IsHeld) return;

    followTarget = holdPoint;
    moveSpeed = followMoveSpeed;
    rotateSpeed = followRotateSpeed;

    // IMPORTANT: zero velocities BEFORE making kinematic
    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;

    // Now make it kinematic / no gravity
    rb.isKinematic = true;
    rb.useGravity = false;

    IsHeld = true;
    transform.SetParent(holdPoint, true);
}
    public void Drop(){
        if (!IsHeld) return;
        IsHeld = false; followTarget = null; transform.SetParent(originalParent, true);
        rb.isKinematic = false; rb.useGravity = true;
        var cam = Camera.main; if (cam) rb.AddForce(cam.transform.forward * 0.5f, ForceMode.VelocityChange);
    }
}