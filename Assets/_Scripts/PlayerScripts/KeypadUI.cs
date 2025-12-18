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

    //public Text textOB;
    public TMP_Text textOB;
    public string correctCode = "69420";
    public UnityEvent onCorrectCode;

    private AudioSource sfxSource;

    public AudioClip buttonClip;
    public AudioClip correctClip;
    public AudioClip wrongClip;

    public bool animate;

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

        textOB.text += number.ToString();

        //AudioManager.Instance.PlaySFX(buttonClip);
        if (sfxSource != null)
            sfxSource.PlayOneShot(buttonClip);
    }

    public void Execute()
    {
        if (textOB.text == correctCode)
        {
            //AudioManager.Instance.PlaySFX(correctClip);
            if (sfxSource != null)
                sfxSource.PlayOneShot(correctClip);
            textOB.text = "Correct!";
            onCorrectCode.Invoke();
        }
        else
        {
            //AudioManager.Instance.PlaySFX(wrongClip);
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
        //AudioManager.Instance.PlaySFX(buttonClip);
        if (sfxSource != null)
            sfxSource.PlayOneShot(buttonClip);
    }

    IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (textOB.text == "Correct!" || textOB.text == "Wrong!")
        {
            textOB.text = "";
        }
    }
}
