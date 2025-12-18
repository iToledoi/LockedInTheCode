using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSequenceManager : MonoBehaviour
{
    public ColorFlash[] sequence;
    public float delayBetweenCrystals = 0.3f;

    private Coroutine sequenceRoutine;

    public void PlaySequence()
    {
        //if (!gameObject.activeInHierarchy)
        //{
        //    Debug.LogError("CrystalSequenceManager is inactive!");
        //    return;
        //}

        if (sequenceRoutine == null)
        {
            Debug.Log("PlaySequence called.");
            sequenceRoutine = StartCoroutine(SequenceRoutine());
        }
    }

    private IEnumerator SequenceRoutine()
    {
        Debug.Log("Starting crystal sequence...");
        foreach (ColorFlash crystal in sequence)
        {
            crystal.Flash();
            yield return new WaitForSeconds(delayBetweenCrystals);
        }

        sequenceRoutine = null;
    }
}
