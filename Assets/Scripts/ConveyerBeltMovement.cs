using UnityEngine;
using System.Collections;

public class ConveyorBeltMovement : MonoBehaviour
{
    public float speed = 5f; 
    public float pauseDuration = 3f; 
    public float positionTolerance = 0.1f; 

    private Rigidbody rb;
    public bool isMoving = false;
    public bool isReversing = false;
    public bool onBelt = false;
    private bool atBackPlate = false;
    private float initialBeltPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            if (isReversing)
            {
                PerformMoveBackward();
            }
            else
            {
                PerformMoveForward();
            }
        }
    }

    public void MoveForward()
    {
        if (onBelt && !atBackPlate) {
            isMoving = true;
            isReversing = false;
        }
    }

    private void PerformMoveForward()
    {
        Vector3 movement = new Vector3(speed * Time.deltaTime, 0, 0);
        rb.MovePosition(transform.position + movement);
    }

    public void MoveBackward()
    {
        if (onBelt) {
            isMoving = true;
            isReversing = true;
        }
    }

    private void PerformMoveBackward()
    {
        Vector3 movement = new Vector3(-speed * Time.deltaTime, 0, 0);
        rb.MovePosition(transform.position + movement);

        // Check if the container is back at the original X position
        if (Mathf.Abs(transform.position.x - initialBeltPosition) <= positionTolerance)
        {
            isMoving = false;
            isReversing = false;
            onBelt = false;
            atBackPlate = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Belt")
        {
            onBelt = true;
            initialBeltPosition = transform.position.x;
        }

        if (collision.gameObject.tag == "belt_back" && !isReversing)
        {
            isMoving = false;
            atBackPlate = true;
        }
    }
}
