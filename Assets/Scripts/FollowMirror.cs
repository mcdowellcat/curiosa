using UnityEngine;

public class FollowMirror : MonoBehaviour
{
    public Transform objectToFollow;  // The object you want mirrored (e.g., player camera)
    public Transform mirror;          // The mirror plane (blue arrow = back)

    void Update()
    {
        // --- Position Reflection ---
        Vector3 localPos = mirror.InverseTransformPoint(objectToFollow.position);
        localPos.x = -localPos.x; // Flip across the mirror plane (X-axis)
        transform.position = mirror.TransformPoint(localPos);

        // --- Rotation Reflection ---
        Vector3 localForward = mirror.InverseTransformDirection(objectToFollow.forward);
        localForward.x = -localForward.x;
        Vector3 mirroredForward = mirror.TransformDirection(localForward);

        Vector3 localUp = mirror.InverseTransformDirection(objectToFollow.up);
        localUp.x = -localUp.x;
        Vector3 mirroredUp = mirror.TransformDirection(localUp);

        transform.rotation = Quaternion.LookRotation(mirroredForward, mirroredUp);
    }
}
