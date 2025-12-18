using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    public string promptMessage;
    public char key;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionPromptManager.Instance.ShowPrompt(promptMessage, key);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionPromptManager.Instance.HidePrompt();
        }
    }
    
    //Outline outline;
    // public string message;
    /*
    public UnityEvent onInteract;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void DisableOutline(){
        outline.enabled = false;
    }

    public void EnableOutline(){
        outline.enabled = true;
    }
    */
}
