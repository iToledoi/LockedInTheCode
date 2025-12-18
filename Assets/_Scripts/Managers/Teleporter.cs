using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    // Trigger the scene change when the player enters the teleporter
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop the timer as the player has escaped
            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                timer.StopTimer();
            }
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
