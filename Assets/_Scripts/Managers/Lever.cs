using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    bool leverDown = false;
    //Animator anim;
    public UnityEvent onPullDown;
    public UnityEvent onPullUp; 

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        leverDown = false;    
    }

    // Lever toggle method
    public void ToggleLever(){
        leverDown = !leverDown;
        //anim.SetBool("leverDown", leverDown);
        if (leverDown){
            Debug.Log("Lever Pulled Down");
            onPullDown.Invoke();
        } else {
            Debug.Log("Lever Pulled Up");
            onPullUp.Invoke();
        }
    }

}
