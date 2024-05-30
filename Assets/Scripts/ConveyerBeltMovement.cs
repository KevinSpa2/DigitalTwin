using UnityEngine;
using System.Collections;

public class ConveyorBeltMovement : MonoBehaviour
{
    public float speed = 5f; 
    public float pauseDuration = 3f; 
    public float positionTolerance = 0.1f; 

    private Rigidbody rb;
    private bool isMoving = false;
    private bool isReversing = false;
    private bool onBelt = false;
    private float initialBeltPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            float direction = isReversing ? -1 : 1;
            Vector3 movement = new Vector3(speed * direction * Time.deltaTime, 0, 0);
            rb.MovePosition(transform.position + movement);

            // Check if the container is back at the original X position
            if (isReversing && Mathf.Abs(transform.position.x - initialBeltPosition) <= positionTolerance)
            {
                isMoving = false;
                isReversing = false;
                onBelt = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Belt")
        {
            // Start moving when in contact with Belt
            onBelt = true;
            isMoving = true;
            initialBeltPosition = transform.position.x;
        }

        if (collision.gameObject.tag == "belt_back" && !isReversing)
        {
            // Stop moving when in contact with object that has belt_back tag
            StartCoroutine(ReverseAfterPause());
        }
    }

    private IEnumerator ReverseAfterPause()
    {
        isMoving = false;
        yield return new WaitForSeconds(pauseDuration);
        isReversing = true;
        if (onBelt)
        {
            isMoving = true;
        }
    }
}