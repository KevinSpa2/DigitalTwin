using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to the container object
public class ReleaseContainer : MonoBehaviour
{
    public WarehouseController warehouseController;

    private bool onShelf = false;

    private void Start()
    {
        warehouseController = FindObjectOfType<WarehouseController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gripper"))
        {
            transform.parent = collision.transform;
            // Set container's parent to the platform
            Debug.Log("Attached to gripper");
            warehouseController.holdingItem = true;
            onShelf = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shelf") && transform.parent != null)
        {
            if (!onShelf)
            {
                // If the container is on the platform, release it
                transform.parent = null;
                Debug.Log("Released container");
                onShelf = true;
                warehouseController.holdingItem = false;
            }
        }
    }
}
