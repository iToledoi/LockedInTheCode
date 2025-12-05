using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    bool buttonPressed = false;
    public UnityEvent onButtonPress;
    
    // Start is called before the first frame update
    void Start()
    {
        buttonPressed = false;
    }

    // Button press method
    public void PressButton(){
        Debug.Log("Button Pressed");
        if (!buttonPressed){
            buttonPressed = true;
            onButtonPress.Invoke();
        }
    }
}
