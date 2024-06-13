using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class StatusBarController : MonoBehaviour
{
    const int STATUS_BAR_ELEMENT_SIZE = 130;
    const int STATUS_BAR_ELEMENT_PADDING = 55;
    const int STATUS_BAR_BACKGROUND_WIDTH = 262;
    const string STATUS_BAR_BUSY_TEXT = "busy";
    const string STATUS_BAR_OK_TEXT = "ok";

    // Singleton
    public static StatusBarController Instance;

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
    private Transform statusBar, statusBackground;

    // List which gets filled with the status buttons
    private List<StatusButton> statusButtons = new List<StatusButton>();

    private List<string> components;

    // Upon starting the program
    void Awake()
    {
        // Assign singleton
        if (Instance == null){
            Instance = this;
        }

        // yPosition to start placing the status buttons
        int yPosition = 940;
        // List containing the required labels for the status buttons
        this.SetStatusBarNames();
        if(components.Count == 0){
            for (int i = 0; i < controller.GetComponent<Transform>().childCount; i++)
            {
                // We get the names from the 3D model
                GameObject child = controller.GetComponent<Transform>().GetChild(i).gameObject;
                components.Add(child.name);
            }
        }
        
        
        // For loop creates the correct number of buttons, background box size needs to be reliant on the number of components in the status bar
        for (int i = 0; i < components.Count; i++)
        {
            // Create the object
            GameObject status = Instantiate(statusButtonPrefab, new Vector3(STATUS_BAR_ELEMENT_SIZE, yPosition, 0), new Quaternion(0, 0, 0, 0), statusBar);
            // Add object to the statusButtons list
            this.statusButtons.Add(status.GetComponent<StatusButton>());
            // Set name
            statusButtons[i].SetComponentName(components[i]);
            // Set status
            statusButtons[i].SetStatusText("ok");
            // Change yPosition for the next button to be placed
            yPosition -= STATUS_BAR_ELEMENT_PADDING;
        }

        statusBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(STATUS_BAR_BACKGROUND_WIDTH, STATUS_BAR_ELEMENT_SIZE + components.Count * 40);
    }

    //Update is called once per frame
    void Update()
    {
        // Check for actions in the warehousecontroller for the greifer
        if (controller.GetComponent<WarehouseController>().isXMoving)
        {
            statusButtons[1].GetStatusButtonImage().sprite = statusButtonOrange;
            statusButtons[1].SetStatusText(STATUS_BAR_BUSY_TEXT);
        }
        else
        {
            statusButtons[1].GetStatusButtonImage().sprite = statusButtonGreen;
            statusButtons[1].SetStatusText(STATUS_BAR_OK_TEXT);
        }

        // Check for actions in the warehousecontroller for the ausleger
        if (controller.GetComponent<WarehouseController>().isYMoving)
        {
            statusButtons[0].GetStatusButtonImage().sprite = statusButtonOrange;
            statusButtons[0].SetStatusText(STATUS_BAR_BUSY_TEXT);
        }
        else
        {
            statusButtons[0].GetStatusButtonImage().sprite = statusButtonGreen;
            statusButtons[0].SetStatusText(STATUS_BAR_OK_TEXT);
        }

        // Check for actions in the warehousecontroller for the turm
        if (controller.GetComponent<WarehouseController>().isZMoving)
        {
            statusButtons[3].GetStatusButtonImage().sprite = statusButtonOrange;
            statusButtons[3].SetStatusText(STATUS_BAR_BUSY_TEXT);
        }
        else
        {
            statusButtons[3].GetStatusButtonImage().sprite = statusButtonGreen;
            statusButtons[3 ].SetStatusText(STATUS_BAR_OK_TEXT);
        }

    }

    public void SetStatusBarNames(){
        components = ConfigurationManager.components;
        for (int i = 0; i < components.Count; i++){
            statusButtons[i].SetComponentName(components[i]);
        }
    }
}