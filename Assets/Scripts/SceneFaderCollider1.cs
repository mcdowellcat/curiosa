using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFaderCollider1 : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;  // fade Image's CanvasGroup 
    public float fadeDuration = 3.0f;    // Duration of fade in/out
    public string sceneToLoad;           // The scene name to load
    public AudioSource audioSource;      // Optional audio
    public string playerTag = "Player";  // Tag of the player object
    private bool hasActivated = false;   // Prevent multiple triggers

   private void OnTriggerEnter(Collider other)
    {
        var root = other.attachedRigidbody ? other.attachedRigidbody.transform.root : other.transform.root;
        if (!hasActivated && root.CompareTag(playerTag))
        {
            hasActivated = true;
            StartCoroutine(FadeAndChangeScene());
        }
    }

    IEnumerator FadeAndChangeScene()
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            yield break;
        }

        // Fade out (skip if no canvas)
        if (fadeCanvasGroup != null)
        {
            yield return StartCoroutine(Fade(1f));
        }
        else
        {
            Debug.LogWarning("[SFC] No fadeCanvasGroup assigned. Skipping fade out.", this);
        }

        SceneManager.LoadScene(sceneToLoad);

        // Give the new scene a moment (realtime so timescale doesn’t matter)
        yield return new WaitForSecondsRealtime(0.5f);

        // Fade in (only if we still exist and have a canvas)
        if (fadeCanvasGroup != null)
        {
            yield return StartCoroutine(Fade(0f));
        }

        if (audioSource != null)
            audioSource.Play();
    }

    IEnumerator Fade(float targetAlpha)
    {
        if (fadeCanvasGroup == null)
        {
            yield break;
        }

        float startAlpha = fadeCanvasGroup.alpha;
        float t = 0f;

        while (t < fadeDuration)
        {
            float p = t / fadeDuration;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, p);
            yield return null;
            t += Time.unscaledDeltaTime; // unscaled in case timeScale==0
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
