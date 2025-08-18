using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
    {
    public CanvasGroup fadeCanvasGroup; // fade Image's CanvasGroup 
    public float fadeDuration = 3.0f;  // Duration of fade in/out
    public string sceneToLoad;  // The scene name to load
    public AudioSource audioSource;

    public void ChangeScene()
    {
        StartCoroutine(FadeAndChangeScene());
    }

    IEnumerator FadeAndChangeScene()
    {
        // Fade out
        yield return StartCoroutine(Fade(1f));

        // Load the scene
        SceneManager.LoadScene(sceneToLoad);

        // Wait until the new scene is loaded
        yield return new WaitForSeconds(0.5f);  // Wait a bit for the scene to load

        // Fade in
        yield return StartCoroutine(Fade(0f));
        audioSource.Play();
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
