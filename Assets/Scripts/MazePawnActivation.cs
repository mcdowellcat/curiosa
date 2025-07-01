using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePawnActivation : MonoBehaviour
{
    public GameObject objectToActivate;
    public AudioSource audioSource;
    private bool hasActivated = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasActivated && collision.gameObject.CompareTag("Ball"))
        {
            objectToActivate.SetActive(true);
            hasActivated = true;

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
