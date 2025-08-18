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

    public void StartMovingUp() => moveDirection = Vector3.up;
    public void StartMovingDown() => moveDirection = Vector3.down;
    public void StartMovingLeft() => moveDirection = Vector3.left;
    public void StartMovingRight() => moveDirection = Vector3.right;

    public void StopMoving() => moveDirection = Vector3.zero;
}