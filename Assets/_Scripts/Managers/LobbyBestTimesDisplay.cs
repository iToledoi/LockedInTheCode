using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyBestTimesDisplay : MonoBehaviour
{
    [SerializeField] private string[] levelIdentifiers;
    [SerializeField] private TextMeshPro[] bestTimeTexts;

    // Display the best times for each level in the lobby
    void Start()
    {
        for (int i = 0; i < levelIdentifiers.Length && i < bestTimeTexts.Length; i++)
        {
            float bestTime = PlayerPrefs.GetFloat("BestTime_" + levelIdentifiers[i], float.MaxValue);
            string timeString;
            if (bestTime == float.MaxValue)
            {
                timeString = "--:--";
            }
            else
            {
                int minutes = Mathf.FloorToInt(bestTime / 60f);
                int seconds = Mathf.FloorToInt(bestTime % 60f);
                timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            bestTimeTexts[i].text = "Best time: " + timeString;
        }
    }
}