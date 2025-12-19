using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{   
    // State variables
    bool inReach = false;
    bool isUp = false;

    // Events for lever actions
    public UnityEvent onPullDown;
    public UnityEvent onPullUp;

    // Interaction key and animator reference
    public KeyCode interactKey = KeyCode.F;
    public Animator anim;

    void Start()
    {
        // Auto-grab Animator on the same object if not set
        if (anim == null)
            anim = GetComponent<Animator>();

        if (anim != null)
            anim.SetBool("isUp", isUp);
    }

    // Method to toggle lever state
    public void ToggleLever()
    {
        isUp = !isUp;

        // Update animator state
        if (anim != null)
            anim.SetBool("isUp", isUp);

        if (isUp)
            onPullUp.Invoke();
        else
            onPullDown.Invoke();
    }

    // Detect player entering/exiting interaction range
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            inReach = true;
    }

    // Detect player exiting interaction range
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inReach = false;
    }

    // Check for interaction input
    void Update()
    {
        if (inReach && Input.GetKeyDown(interactKey))
            ToggleLever();
    }
}
