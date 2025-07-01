using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    public float moveSpeed = 0.3f; // units per second
    private Vector3 moveDirection = Vector3.zero;
    public AudioSource audioSource;

    void Update()
    {
        if (moveDirection != Vector3.zero)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    public void StartMovingUp() => moveDirection = Vector3.up;
    public void StartMovingDown() => moveDirection = Vector3.down;
    public void StartMovingLeft() => moveDirection = Vector3.left;
    public void StartMovingRight() => moveDirection = Vector3.right;

    public void StopMoving() => moveDirection = Vector3.zero;
}
