using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
//using UnityStandardAssets.Characters.FirstPerson;

public class KeypadUI : MonoBehaviour
{
    public GameObject player;
    public GameObject keypadOB;
    public GameObject hud;
    public GameObject inv;

    public GameObject animateOB;
    public Animator ANI;

    public TMP_Text textOB;
    public string correctCode = "69420";
    public UnityEvent onCorrectCode;

    private AudioSource sfxSource;

    public AudioClip buttonClip;
    public AudioClip correctClip;
    public AudioClip wrongClip;

    public bool animate;

    //initialize audio source
    void Awake()
    {
        sfxSource = GetComponent<AudioSource>();
    }

    public void Number(int number)
    {
        //if user presses a number button while "Correct" or "Wrong" is displayed, clear it first
        if (textOB.text == "Correct!" || textOB.text == "Wrong!")
        {
            textOB.text = "";
        }

        textOB.text += number.ToString();       //add number to keypad display

        //check if audio source is assigned first
        if (sfxSource != null)
            sfxSource.PlayOneShot(buttonClip);
    }

    //check if entered code is correct;
    //if correct, invoke onCorrectCode event;
    //called when user presses "Enter" button
    public void Execute()
    {
        if (textOB.text == correctCode)
        {
            if (sfxSource != null)
                sfxSource.PlayOneShot(correctClip);

            textOB.text = "Correct!";
            onCorrectCode.Invoke();
        }
        else
        {
            if (sfxSource != null)
                sfxSource.PlayOneShot(wrongClip);

            textOB.text = "Wrong!";
        }

        //clear keypad after 1 second, if it hasn't been cleared already
        StartCoroutine(ClearTextAfterDelay());
    }

    public void Clear()
    {
        textOB.text = "";
        if (sfxSource != null)
            sfxSource.PlayOneShot(buttonClip);
    }

    //clear "Correct!" or "Wrong!" after 1 second delay
    IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (textOB.text == "Correct!" || textOB.text == "Wrong!")
        {
            textOB.text = "";
        }
    }
}
