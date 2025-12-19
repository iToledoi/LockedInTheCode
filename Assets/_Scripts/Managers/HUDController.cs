using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{   
    public static HUDController instance;
    
    // Singleton pattern implementation
    private void Awake(){
        if (instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Reference to the interaction text UI element
    [SerializeField] private TMP_Text interactionText;

    // Method to enable interaction text with a specific message
    public void EnableInteractionText(string message){
        interactionText.text = message + " (F)";
        interactionText.gameObject.SetActive(true);
    }
    
    // Method to disable interaction text
    public void DisableInteractionText(){
        interactionText.gameObject.SetActive(false);
    }
}
