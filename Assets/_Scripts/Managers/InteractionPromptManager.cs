using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class InteractionPromptManager : MonoBehaviour
{
    public static InteractionPromptManager Instance;

    //assign in inspector
    public TMP_Text promptText;

    private void Awake()
    {
        //create one instance of the manager that persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            promptText.gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //show a prompt
    public void ShowPrompt(string message, char key)
    {
        promptText.text = $"{message} \n [{key}]";
        promptText.gameObject.SetActive(true);
    }

    // hide the prompt
    public void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }
}
