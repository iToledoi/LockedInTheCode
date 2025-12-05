using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{   
    public static HUDController instance;
    
    private void Awake(){
        if (instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    [SerializeField] private TMP_Text interactionText;

    public void EnableInteractionText(string message){
        interactionText.text = message + " (F)";
        interactionText.gameObject.SetActive(true);
    }
    
    public void DisableInteractionText(){
        interactionText.gameObject.SetActive(false);
    }
}
