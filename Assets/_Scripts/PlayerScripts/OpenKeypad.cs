using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeypad : MonoBehaviour
{
    public GameObject keypadOB;
    public GameObject keypadText;

    public bool inReach;
    
    // Start is called before the first frame update
    void Start()
    {
        inReach = false;
        keypadOB.SetActive(false);
        keypadText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("In Reach");
            inReach = true;
            keypadText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Out of Reach");
            inReach = false;
            keypadText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //open keypad with E
        if (Input.GetKeyDown(KeyCode.E) && inReach)
        {
            Debug.Log("Open Keypad");
            keypadOB.SetActive(true);
            keypadText.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //close keypad with escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //set keypadText active only if keypadOB (the UI) was active
            if (keypadOB.activeSelf == true)
                keypadText.SetActive(true);
            keypadOB.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
