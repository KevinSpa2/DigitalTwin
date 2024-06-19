using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to the container object
public class ReleaseContainer : MonoBehaviour
{
    public WarehouseController warehouseController;

    private bool onShelf = false;

    private string shelfName;

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
            Debug.Log("Set holdingItem to true");
            warehouseController.setHoldingItem(true);
            onShelf = false;
            if (shelfName != null)
            {
                warehouseController.shelfManager.RemoveValue(shelfName);
            }
        } 
        else if (collision.gameObject.CompareTag("Belt"))
        {
            Debug.Log("Released container on BELT");
            warehouseController.setHoldingItem(false);
            transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shelf"))
        {
            shelfName = other.gameObject.name;
            if (transform.parent != null)
            {
                if (!onShelf)
                {
                    // If the container is on the platform, release it
                    transform.parent = null;
                    Debug.Log("Released container");
                    onShelf = true;
                    warehouseController.setHoldingItem(false);
                    warehouseController.shelfManager.AddValue(shelfName);
                }
            }
            else
            {
                warehouseController.shelfManager.AddValue(shelfName);
            }
        }
    }
}
