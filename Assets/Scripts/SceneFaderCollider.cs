using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFaderCollider : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;  // fade Image's CanvasGroup 
    public float fadeDuration = 3.0f;    // Duration of fade in/out
    public string sceneToLoad;           // The scene name to load
    public AudioSource audioSource;      // Optional audio
    public string playerTag = "Player";  // Tag of the player object
    private bool hasActivated = false;   // Prevent multiple triggers

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag(playerTag))
        {
            hasActivated = true;
            StartCoroutine(FadeAndChangeScene());
        }
    }

    IEnumerator FadeAndChangeScene()
    {
        // Fade out
        yield return StartCoroutine(Fade(1f));

        // Load the scene

        Debug.Log($"[SceneFaderCollider] Loading scene: {sceneToLoad}");

        SceneManager.LoadScene(sceneToLoad);

        // Wait a bit for the scene to load
        yield return new WaitForSeconds(0.5f);

        // Fade in
        yield return StartCoroutine(Fade(0f));

        // Play audio if assigned
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            yield return null;
            timeElapsed += Time.deltaTime;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
