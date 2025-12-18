using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private string levelIdentifier = "Default";
    private float elapsedTime = 0f;
    private bool isRunning = true;
    private float bestTime;

    // Set the best time key based on level identifier
    void Start()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime_" + levelIdentifier, float.MaxValue);
    }

    //if the timer is running, update the elapsed time and display it
    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Stop the timer and check for best time
    public void StopTimer()
    {
        isRunning = false;
        if (elapsedTime < bestTime)
        {
            bestTime = elapsedTime;
            PlayerPrefs.SetFloat("BestTime_" + levelIdentifier, bestTime);
            PlayerPrefs.Save();
        }
    }

    // Get the best time for the current level
    public float GetBestTime()
    {
        return bestTime;
    }

    // Reset the best time for the current level
    public void ResetBestTime()
    {
        PlayerPrefs.DeleteKey("BestTime_" + levelIdentifier);
        bestTime = float.MaxValue;
    }
}
