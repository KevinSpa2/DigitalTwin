using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject controller;
    public Image statusButton;
    public Sprite statusButtonOrange , statusButtonGreen;


    // Update is called once per frame
    void Update()
    {
        // Controleer of er acties worden uitgevoerd in de WarehouseController
        if (controller.GetComponent<WarehouseController>().isZMoving ||
            controller.GetComponent<WarehouseController>().isYMoving ||
            controller.GetComponent<WarehouseController>().isXMoving
        ){
            statusButton.sprite = statusButtonOrange;
        }
        else
        {
            statusButton.sprite = statusButtonGreen;
        }
    }
}