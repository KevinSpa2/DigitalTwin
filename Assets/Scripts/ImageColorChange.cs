using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class NewBehaviourScript : MonoBehaviour
{
    // Warehousecontroller
    [SerializeField]
    private GameObject controller;

    // Different button states
    [SerializeField]
    private Sprite statusButtonOrange, statusButtonGreen, statusButtonRed;

    // Prefab for status button generation
    [SerializeField]
    private GameObject statusButtonPrefab;

    // Parent of the status buttons
    [SerializeField]
    private Transform statusBar;

    // List which gets filled with the status buttons
    private List<StatusButton> statusButtons = new List<StatusButton>();

    // Upon starting the program
    void Awake()
    {
        // yPosition to start placing the status buttons
        int yPosition = 940;
        // List containing the required labels for the status buttons
        List<string> components = new List<string>();
        for(int i = 0; i < controller.GetComponent<Transform>().childCount; i++)
        {
            // We get the names from the 3D model
            GameObject child = controller.GetComponent<Transform>().GetChild(i).gameObject;
            components.Add(child.name);
        }
        
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
            statusButtons[1].GetStatusButtonImage().sprite = statusButtonOrange;
            statusButtons[1].SetStatusText("busy");
        }
        else
        {
            statusButtons[1].GetStatusButtonImage().sprite = statusButtonGreen;
            statusButtons[1].SetStatusText("ok");
        }

        // Controleer of er acties worden uitgevoerd in de WarehouseController voor de Boom
        if (controller.GetComponent<WarehouseController>().isYMoving)
        {
            statusButtons[0].GetStatusButtonImage().sprite = statusButtonOrange;
            statusButtons[0].SetStatusText("busy");
        }
        else
        {
            statusButtons[0].GetStatusButtonImage().sprite = statusButtonGreen;
            statusButtons[0].SetStatusText("ok");
        }

        // Controleer of er acties worden uitgevoerd in de WarehouseController voor de Toren
        if (controller.GetComponent<WarehouseController>().isZMoving)
        {
            statusButtons[3].GetStatusButtonImage().sprite = statusButtonOrange;
            statusButtons[3].SetStatusText("busy");
        }
        else
        {
            statusButtons[3].GetStatusButtonImage().sprite = statusButtonGreen;
            statusButtons[3 ].SetStatusText("ok");
        }

    }
}