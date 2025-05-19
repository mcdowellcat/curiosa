using UnityEngine;

public class PlayAudioOnSelect : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayAudio()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
