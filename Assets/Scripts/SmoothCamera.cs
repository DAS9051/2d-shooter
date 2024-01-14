using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    public Transform target;

    private Vector3 vel = Vector3.zero;
    private bool isFollowing = true; // Add a flag to control the following behavior

    private void FixedUpdate()
    {
        if (!isFollowing || target == null) // Check if following is enabled
            return;

        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
    }

    // Public method to stop the camera from following the target
    public void StopFollowing()
    {
        isFollowing = false;
    }
}
