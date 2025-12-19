using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script will make an object pulse between two colors when attached to it
public class ColorFlash : MonoBehaviour
{
    public Color flashColor = Color.green;
    public float fadeDuration = 0.5f;

    private Renderer ren;
    private Color defaultColor;

    private Coroutine flashCoroutine;

    //initialize renderer and default color
    void Awake()
    {
        ren = GetComponent<Renderer>();
        defaultColor = ren.material.color;
    }

    //start the flash effect
    public void Flash()
    {
        // check if coroutine is already running
        if (flashCoroutine == null)
            flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // instantly switch color
        ren.material.color = flashColor;

        // fade back to default color overtime
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            ren.material.color = Color.Lerp(flashColor, defaultColor, t / fadeDuration);
            yield return null;
        }

        // end coroutine and reset color
        flashCoroutine = null;
        ren.material.color = defaultColor;
    }
}
