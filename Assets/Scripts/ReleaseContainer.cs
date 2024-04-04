using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to the container object
public class ReleaseContainer : MonoBehaviour
{
    public WarehouseController warehouseController;
    public GripperItems gripperItems;

    private bool onShelf = false;

    private void Start()
    {
        warehouseController = FindObjectOfType<WarehouseController>();
        gripperItems = FindObjectOfType<GripperItems>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shelf") && transform.parent != null)
        {
            if (!onShelf)
            {
                // If the container is on the platform, release it
                transform.parent = null;
                gripperItems.container = null;
                onShelf = true;
                Debug.Log("Released container");
                warehouseController.holdingItem = false;
            }
        }
    }
}
