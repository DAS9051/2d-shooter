using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset; // Offset from the target position
    [SerializeField] private float damping; // Damping effect for smooth movement

    public Transform target; // The target that the camera will follow

    private Vector3 vel = Vector3.zero; // Velocity used by the SmoothDamp function
    private bool isFollowing = true; // Flag to control whether the camera is following the target

    private void FixedUpdate()
    {
        // Check if the camera is set to follow and if there is a target
        if (!isFollowing || target == null)
            return; // If not following or no target, exit the method

        // Calculate the target position with the offset
        Vector3 targetPosition = target.position + offset;
        // Keep the camera's original Z position
        targetPosition.z = transform.position.z;

        // Smoothly move the camera towards the target position using SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
    }

    // Public method to stop the camera from following the target
    public void StopFollowing()
    {
        isFollowing = false; // Set the following flag to false
    }
}
