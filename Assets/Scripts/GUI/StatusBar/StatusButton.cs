using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StatusButton : MonoBehaviour
{
    // Fields
    public Image statusButtonImage;
    public TMP_Text componentName, statusText;

    // Constructor
    public StatusButton(Image statusButtonImage, string componentName, string statusText)
    {
        this.statusButtonImage = statusButtonImage;
        this.componentName.text = componentName;
        this.statusText.text = statusText;
    }

    // Getters & Setters
    public Image GetStatusButtonImage()
    {
        return this.statusButtonImage;
    }

    public void SetStatusButtonImage(Image statusButtonImage)
    {
        this.statusButtonImage = statusButtonImage;
    }

    public TMP_Text GetComponentName()
    {
        return this.componentName;
    }

    public void SetComponentName(string componentName)
    {
        this.componentName.text = componentName;
    }

    public TMP_Text GetStatusText()
    {
        return this.statusText;
    }

    public void SetStatusText(string statusText)
    {
        this.statusText.text = statusText;
    }
}
