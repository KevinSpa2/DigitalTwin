using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraScript : MonoBehaviour
{
    public Camera[] cameras;
    public int currentCameraIndex;

    void Start()
    {
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }

    public void ChangeCameraAngleRight()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);

        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        cameras[currentCameraIndex].gameObject.SetActive(true);
    }

    public void ChangeCameraAngleLeft()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);

        currentCameraIndex--;
        if (currentCameraIndex < 0)
        {
            currentCameraIndex = cameras.Length - 1;
        }

        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
