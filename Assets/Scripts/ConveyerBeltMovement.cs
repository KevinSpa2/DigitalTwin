using UnityEngine;
using System.Collections;

public class ConveyorBeltMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private bool isMoving = false;
    private bool onBelt = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 movement = new Vector3(speed * Time.deltaTime, 0, 0);
            rb.MovePosition(transform.position + movement);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Belt")
        {
            // Start moving when in contact with Belt
            onBelt = true;
            isMoving = true;
        }

        if (collision.gameObject.tag == "belt_back")
        {
            // Stop moving when in contact with object that has belt_back tag
            isMoving = false;
            onBelt = false;
        }
    }
}
