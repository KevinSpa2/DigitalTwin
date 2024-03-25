using System.Collections;
using UnityEngine;

public class MoveXPosition : MonoBehaviour
{
    public float speed = 1.0f; // Adjust the speed as needed
    public float maxX = -5.21f;
    public float minX = -11.1655f;
    public float bufferZone = 0.1f; // Add a buffer zone to boundary checks


    private bool movingToMax = true; // True when moving towards maxX, false when moving towards minX

    private bool isMoving = false; // Flag to indicate whether the coroutine is currently running

    // Function to call when you want to start the movement
    public void StartMovement()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine());
        }
    }

private IEnumerator MoveCoroutine()
    {
        isMoving = true;

        while (true)
        {   
            Debug.LogWarning("Started");
            if (movingToMax)
            {
                // Move the object towards maxX
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                if (transform.position.x + bufferZone >= maxX) // Add buffer zone
                {
                    // If reached maxX, start moving towards minX
                    movingToMax = false;
                }
            }
            else
            {
                // Move the object towards minX
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                if (transform.position.x - bufferZone <= minX) // Add buffer zone
                {
                    // If reached minX, stop the movement
                    break;
                }
            }
            yield return null;
        }
        Debug.LogWarning("Stopped");
        isMoving = false;
    }
}
