using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    void Start()
    {
        // Example: Start with a fade-in
        StartCoroutine(FadeIn());
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(FadeOut());
        }
    }
    public IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = 1 - (elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0; // Ensure alpha is 0 at the end
    }

    public IEnumerator FadeOut()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = elapsed / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 1; // Ensure alpha is 1 at the end
    }
}
