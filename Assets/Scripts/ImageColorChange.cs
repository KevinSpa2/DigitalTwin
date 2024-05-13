using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject controller;
    public Sprite statusButtonOrange, statusButtonGreen;
    public StatusButton statusButton;
    public GameObject statusButtonPrefab;
    public Transform statusBar;

    public List<StatusButton> statusButtons = new List<StatusButton>();

    void Awake()
    {
        int yPosition = 950;
        List<string> components = new List<string>{"Grijper", "Loopband", "Toren", "Boom"};
        
        for(int i = 0; i < 4; i++)
        {
            GameObject status = Instantiate(statusButtonPrefab, new Vector3(120, yPosition, 0), new Quaternion(0, 0, 0, 0), statusBar);
            this.statusButtons.Add(status.GetComponent<StatusButton>());
            statusButtons[i].SetComponentName(components[i]);
            yPosition -= 50;
        }


    }

    // controller.GetComponent<WarehouseController>().isZMoving || Toren
    // controller.GetComponent<WarehouseController>().isYMoving || Boom
    // controller.GetComponent<WarehouseController>().isXMoving  grijper
    //Update is called once per frame
    void Update()
    {
        // Controleer of er acties worden uitgevoerd in de WarehouseController voor de grijper
        if (controller.GetComponent<WarehouseController>().isXMoving)
        {
             statusButtons[0].GetStatusButtonImage().sprite = statusButtonOrange;
        }
        else
        {
            statusButtons[0].GetStatusButtonImage().sprite = statusButtonGreen;
        }

        // Controleer of er acties worden uitgevoerd in de WarehouseController voor de Boom
        if (controller.GetComponent<WarehouseController>().isYMoving)
        {
             statusButtons[3].GetStatusButtonImage().sprite = statusButtonOrange;
        }
        else
        {
            statusButtons[3].GetStatusButtonImage().sprite = statusButtonGreen;
        }

        // Controleer of er acties worden uitgevoerd in de WarehouseController voor de Toren
        if (controller.GetComponent<WarehouseController>().isZMoving)
        {
             statusButtons[2].GetStatusButtonImage().sprite = statusButtonOrange;
        }
        else
        {
            statusButtons[2].GetStatusButtonImage().sprite = statusButtonGreen;
        }

    }
}