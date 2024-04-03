using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to the container object
public class ReleaseContainer : MonoBehaviour
{
    public WarehouseController warehouseController;
    private void Start()
    {
        warehouseController = FindObjectOfType<WarehouseController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shelf") && transform.parent != null)
        {
            // If the container is on the platform, release it
            transform.parent = null; // Detach from parent
            Debug.Log("Released container from RELEASE");
            warehouseController.holdingItem = false;
        }
    }
}
