using UnityEngine;

public class BallMoverJoystickOpposite : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 0.3f; // units per second
    private Vector3 moveDirection = Vector3.zero;

    [Header("Joystick Input")]
    [Tooltip("Optional joystick reference. If set, joystick input overrides button input.")]
    public UnityEngine.XR.Content.Interaction.XRJoystick joystick;

    [Header("Audio")]
    public AudioSource audioSource;
    private bool wasMovingLastFrame = false;

    void Update()
    {
        // If joystick is assigned, use its input
        if (joystick != null)
        {
            Vector2 joystickValue = joystick.value;  // X = left/right, Y = forward/back
            // Invert the controls here
            moveDirection = new Vector3(-joystickValue.x, -joystickValue.y, 0);
        }

        bool isMoving = moveDirection != Vector3.zero;

        // -- Move the ball --
        if (isMoving)
        {
            transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
        }

        // -- Handle audio --
        if (isMoving && !wasMovingLastFrame)  // movement just started
        {
            if (audioSource && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (!isMoving && wasMovingLastFrame) // movement just stopped
        {
            if (audioSource && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        wasMovingLastFrame = isMoving;
    }

    // Button-based controls (still work if joystick is not assigned)
    public void StartMovingUp() => moveDirection = Vector3.up;
    public void StartMovingDown() => moveDirection = Vector3.down;
    public void StartMovingLeft() => moveDirection = Vector3.left;
    public void StartMovingRight() => moveDirection = Vector3.right;
    public void StopMoving() => moveDirection = Vector3.zero;

    // Joystick input (for external calls if needed)
    public void SetJoystickInput(float x, float y) => moveDirection = new Vector3(-x, -y, 0); // also inverted here
}
