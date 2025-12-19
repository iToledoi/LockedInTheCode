using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{   
    // Message to display when player is in range
    public string promptMessage;
    public char key;

    // Detect when player enters interaction range
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionPromptManager.Instance.ShowPrompt(promptMessage, key);
        }
    }

    // Detect when player exits interaction range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionPromptManager.Instance.HidePrompt();
        }
    }
    
}
