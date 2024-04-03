using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripperItems : MonoBehaviour
{
    private Transform container; // Reference to the container being picked up
    public WarehouseController warehouseController;
    private void Start()
    {
        warehouseController = FindObjectOfType<WarehouseController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Container") && container == null)
        {
            container = collision.transform;
            // Set container's parent to the platform
            container.SetParent(transform); 
            Debug.Log("Grabbed container");
            warehouseController.holdingItem = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shelf") && container != null)
        {
            container.DetachChildren(); 
            container = null; 
            Debug.Log("Released container");
            warehouseController.holdingItem = false;
        }
    }
}
