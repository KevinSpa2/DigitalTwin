using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    // The UI object that needs to be opened or closed
    public GameObject popUp;

    // Method for opening the UI element
    public void OpenPopUp()
    {
        popUp.SetActive(true);
    }

    // Method for closing the UI element
    public void ClosePopUp()
    {
        popUp.SetActive(false);
    }
}
