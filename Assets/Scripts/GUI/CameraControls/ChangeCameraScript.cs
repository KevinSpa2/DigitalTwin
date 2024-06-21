using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraScript : MonoBehaviour
{

    // Singleton
    public static ChangeCameraScript Instance;

    // Array of all camera's
    public Camera[] cameras;
    
    // Array index of the currently used camera
    public int currentCameraIndex;

    // Currently used camera
    private Camera currentCamera;

    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this;
        }
    }

    void Start()
    {
        // Disable all camera's except cameras[0]
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        currentCameraIndex = 0;
        currentCamera = cameras[currentCameraIndex];
    }

    // Method for the right arrow button
    public void ChangeCameraAngleRight()
    {
        currentCamera.gameObject.SetActive(false);

        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        currentCamera = cameras[currentCameraIndex];

        currentCamera.gameObject.SetActive(true);
    }

    // Method for the left arrow button
    public void ChangeCameraAngleLeft()
    {
        if (cameras.Length == 0) 
        {
            return;
        }

        currentCamera.gameObject.SetActive(false);

        currentCameraIndex--;
        if (currentCameraIndex < 0)
        {
            currentCameraIndex = cameras.Length - 1;
        }

        currentCamera = cameras[currentCameraIndex];
        currentCamera.gameObject.SetActive(true);
    }

    // Method to set a specific camera, useful for camera's in the statusbar (unimplemented)
    public void ChangeToSpecificCamera(Camera camera)
    {
        currentCamera.gameObject.SetActive(false);
        currentCamera = camera;
        currentCamera.gameObject.SetActive(true);
    }
}
