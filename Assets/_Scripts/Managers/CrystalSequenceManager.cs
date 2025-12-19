using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSequenceManager : MonoBehaviour
{
    public ColorFlash[] sequence;               //assign objects in inspector to flash in sequence
    public float delayBetweenCrystals = 0.3f;

    private Coroutine sequenceRoutine;

    // start playing the flash sequence
    public void PlaySequence()
    {
        if (sequenceRoutine == null)
        {
            sequenceRoutine = StartCoroutine(SequenceRoutine());
        }
    }

    private IEnumerator SequenceRoutine()
    {
        // iterate through and flash each crystal in sequence with delay
        foreach (ColorFlash crystal in sequence)
        {
            crystal.Flash();
            yield return new WaitForSeconds(delayBetweenCrystals);
        }

        sequenceRoutine = null;
    }
}
