using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    public float moveSpeed = 0.3f; // units per second
    private Vector3 moveDirection = Vector3.zero;
    bool wasMovingLastFrame = false;   // tracks state change
    public AudioSource audioSource;

    void Update()
    {
        bool isMoving = moveDirection != Vector3.zero;

        // ‑‑ move the ball ‑‑
        if (isMoving)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }


        // ‑‑ handle audio ‑‑
        if (isMoving && !wasMovingLastFrame)  // movement just started
        {
            if (audioSource && !audioSource.isPlaying)
            {
                audioSource.Play();  // start / resume
            }
        }

        else if (!isMoving && wasMovingLastFrame) // movement just stopped
        {
            if (audioSource && audioSource.isPlaying)
            {
                audioSource.Stop();  // silence
            }
            
        }

        wasMovingLastFrame = isMoving;
    }
    
    
    public void SetJoystickInput(float x, float y)
    {
        moveDirection = new Vector3(x, y, 0).normalized;
    }
}
