using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class NewBehaviourScript : MonoBehaviour
{
    // Warehousecontroller
    public GameObject controller;

    // Different button states
    public Sprite statusButtonOrange, statusButtonGreen;

    // Prefab for status button generation
    public GameObject statusButtonPrefab;

    // Parent of the status buttons
    public Transform statusBar;

    // List which gets filled with the status buttons
    public List<StatusButton> statusButtons = new List<StatusButton>();

    // Upon starting the program
    void Awake()
    {
        // yPosition to start placing the status buttons
        int yPosition = 940;
        // List containing the required labels for the status buttons, this still needs to be made modular/scalable
        List<string> components = new List<string>{"Grijper", "Loopband", "Toren", "Boom"};
        
        // For loop creates the correct number of buttons, background box size needs to be reliant on the number of components in the status bar
        for(int i = 0; i < components.Count; i++)
        {
            // Create the object
            GameObject status = Instantiate(statusButtonPrefab, new Vector3(130, yPosition, 0), new Quaternion(0, 0, 0, 0), statusBar);
            // Add object to the statusButtons list
            this.statusButtons.Add(status.GetComponent<StatusButton>());
            // Set name
            statusButtons[i].SetComponentName(components[i]);
            // Set status
            statusButtons[i].SetStatusText("ok");
            // Change yPosition for the next button to be placed
            yPosition -= 55;
        }


    }

    //Update is called once per frame
    void Update()
    {
        // Controleer of er acties worden uitgevoerd in de WarehouseController voor de grijper
        if (controller.GetComponent<WarehouseController>().isXMoving)
        {
            statusButtons[0].GetStatusButtonImage().sprite = statusButtonOrange;
            statusButtons[0].SetStatusText("busy");
        }
        else
        {
            statusButtons[0].GetStatusButtonImage().sprite = statusButtonGreen;
            statusButtons[0].SetStatusText("ok");
        }

        // Controleer of er acties worden uitgevoerd in de WarehouseController voor de Boom
        if (controller.GetComponent<WarehouseController>().isYMoving)
        {
            statusButtons[3].GetStatusButtonImage().sprite = statusButtonOrange;
            statusButtons[3].SetStatusText("busy");
        }
        else
        {
            statusButtons[3].GetStatusButtonImage().sprite = statusButtonGreen;
            statusButtons[3].SetStatusText("ok");
        }

        // Controleer of er acties worden uitgevoerd in de WarehouseController voor de Toren
        if (controller.GetComponent<WarehouseController>().isZMoving)
        {
            statusButtons[2].GetStatusButtonImage().sprite = statusButtonOrange;
            statusButtons[2].SetStatusText("busy");
        }
        else
        {
            statusButtons[2].GetStatusButtonImage().sprite = statusButtonGreen;
            statusButtons[2].SetStatusText("ok");
        }

    }
}