using UnityEngine;

public class MirrorMovement : MonoBehaviour
{
    public Transform playerTarget; // The player's camera
    public Transform mirror;       // The mirror plane (blue Z-axis points out the back)
    public float surfaceOffset = 0f; // Distance from the pivot to the visible surface of the mirror

    void Update()
    {
        // Step 1: Calculate player position relative to mirror pivot
        Vector3 localPlayerPos = mirror.InverseTransformPoint(playerTarget.position);

        // Step 2: Flip across the mirror plane
        localPlayerPos.x = -localPlayerPos.x;

        // Step 3: Convert back to world space
        Vector3 mirroredWorldPos = mirror.TransformPoint(localPlayerPos);

        // Step 4: Shift along mirror surface normal if pivot is not on the surface
        mirroredWorldPos += mirror.forward * surfaceOffset;

        transform.position = mirroredWorldPos;

        // --- Mirror rotation ---
        Vector3 localForward = mirror.InverseTransformDirection(playerTarget.forward);
        localForward.x = -localForward.x;
        Vector3 mirroredForward = mirror.TransformDirection(localForward);

        Vector3 localUp = mirror.InverseTransformDirection(playerTarget.up);
        localUp.x = -localUp.x;
        Vector3 mirroredUp = mirror.TransformDirection(localUp);

        transform.rotation = Quaternion.LookRotation(mirroredForward, mirroredUp);
    }
}
