using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject popUp;

    public void openPopUp()
    {
        popUp.SetActive(true);
    }

    public void closePopUp()
    {
        popUp.SetActive(false);
    }
}
