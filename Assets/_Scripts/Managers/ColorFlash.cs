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

    void Awake()
    {
        ren = GetComponent<Renderer>();
        defaultColor = ren.material.color;
    }

    public void Flash()
    {
        if (flashCoroutine == null)
        {
            Debug.Log("ColorFlash: Starting flash.");
            flashCoroutine = StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        // instantly switch color
        ren.material.color = flashColor;

        float t = 0f;
        while (t < fadeDuration)
        {
            Debug.Log("ColorFlash: Fading... " + (t / fadeDuration));
            t += Time.deltaTime;
            ren.material.color = Color.Lerp(flashColor, defaultColor, t / fadeDuration);
            yield return null;
        }

        flashCoroutine = null;
        ren.material.color = defaultColor;
    }
}
