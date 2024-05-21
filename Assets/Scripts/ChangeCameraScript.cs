using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraScript : MonoBehaviour
{
    public Camera[] cameras;
    public int currentCameraIndex;
    private Camera currentCamera;

    void Start()
    {
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        currentCameraIndex = 0;
        currentCamera = cameras[currentCameraIndex];
    }

    public void ChangeCameraAngleRight()
    {
        currentCamera.gameObject.SetActive(false);

        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        currentCamera = cameras[currentCameraIndex];

        currentCamera.gameObject.SetActive(true);
    }

    public void ChangeCameraAngleLeft()
    {
        if (cameras.Length == 0) return;

        currentCamera.gameObject.SetActive(false);

        currentCameraIndex--;
        if (currentCameraIndex < 0)
        {
            currentCameraIndex = cameras.Length - 1;
        }
        currentCamera = cameras[currentCameraIndex];
        currentCamera.gameObject.SetActive(true);
    }

    public void ChangeToSpecificCamera(Camera camera)
    {
        currentCamera.gameObject.SetActive(false);
        currentCamera = camera;
        currentCamera.gameObject.SetActive(true);
    }
}
