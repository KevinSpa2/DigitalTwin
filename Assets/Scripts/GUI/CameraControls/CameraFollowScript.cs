using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform target;  // The target for the camera to follow
    public float smoothSpeed = 0.125f;  // Smoothing speed for camera movement
    public Vector3 offset;  // Offset from the target position

    void LateUpdate()
    {
        // Calculate the desired position based on the target's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
