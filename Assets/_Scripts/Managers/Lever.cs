using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    bool inReach = false;
    bool isUp = false;

    public UnityEvent onPullDown;
    public UnityEvent onPullUp;

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

    public void ToggleLever()
    {
        isUp = !isUp;

        if (anim != null)
            anim.SetBool("isUp", isUp);

        if (isUp)
            onPullUp.Invoke();
        else
            onPullDown.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            inReach = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inReach = false;
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(interactKey))
            ToggleLever();
    }
}
