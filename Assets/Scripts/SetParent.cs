using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    private Transform container; // Reference to the container being picked up

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Container") && container == null)
        {
            container = collision.transform;
            container.SetParent(transform); // Set container's parent to the platform
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shelf") && container != null)
        {
            container.SetParent(other.transform); // Set container's parent to the shelf
            container = null; // Reset container reference
        }
    }
}
