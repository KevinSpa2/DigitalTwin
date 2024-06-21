using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEditor;

public class ConfigurationManager : MonoBehaviour
{
    const int STANDARD_Y_POSITION = 450;

    [SerializeField]
    private Transform modelParent, modelOptionsParent, subModuleParent, cameraFieldParent, cameraParent;

    [SerializeField]
    private List<GameObject> models;

    [SerializeField]
    private GameObject subModuleFieldPrefab, cameraFieldPrefab, cameraPrefab;

    [SerializeField]
    private TMP_InputField netIdField, portField;

    [SerializeField]
    private PlcController plcController;

    private string inputText;
    private int yPosition;
    private List<GameObject> subModules, children, cameraFields;
    private GameObject selectedModel;

    // This list gets used in StatusBarController.cs for dynamic submodule names
    public static List<string> components = new List<string>();

    public string savedNetId;
    public int savedPort;

    // Preset texts in input fields for PLC connection
    public void Awake()
    {
        this.netIdField.text = "127.0.0.1.1.1";
        this.portField.text = "851";
    }

    // Generate all necessary elements
    public void SelectModel(int index)
    {
        this.Reset();

        // Load the 3D model into the scene
        selectedModel = Instantiate(models[index], new Vector3(0, 11.02f, 0), Quaternion.identity, modelParent);
        models[index] = selectedModel;

        // Find all children of the 3D model
        this.children = new List<GameObject>();
        for (int i = 0; i < selectedModel.transform.childCount; i++)
        {
            GameObject child = selectedModel.transform.GetChild(i).gameObject;
            children.Add(child);
            components.Add(child.name);
        }

        // Generate the inputfields for the submodules and camera's
        yPosition = STANDARD_Y_POSITION;
        this.subModules = new List<GameObject>();
        this.cameraFields = new List<GameObject>();
        for (int i = 0; i < components.Count; i++)
        {
            GameObject subModule = Instantiate(subModuleFieldPrefab, new Vector3(1200, yPosition, 0), Quaternion.identity, subModuleParent);
            GameObject cameraField = Instantiate(cameraFieldPrefab, new Vector3(1000, yPosition - 10, 0), Quaternion.identity, cameraFieldParent);
            subModule.GetComponent<TMP_InputField>().text = components[i];
            subModules.Add(subModule);
            cameraFields.Add(cameraField);
            yPosition -= 85;
        }
    }

    // Save input in inputfields
    public void SaveSubModuleNames(){
        for (int i = 0; i < selectedModel.transform.childCount; i++)
        {
            components[i] = subModules[i].GetComponent<TMP_InputField>().text;
        }

        StatusBarController.Instance.SetStatusBarNames();

        for (int i = 0; i < cameraFields.Count; i++)
        {
            // The input fields of the camera of each submodule, each array has length 3 and contains x, y and z.
            TMP_InputField[] fields = cameraFields[i].GetComponentsInChildren<TMP_InputField>(true);
            Vector3 cameraPosition = new Vector3(float.Parse(fields[0].text), float.Parse(fields[1].text), float.Parse(fields[2].text));
            GameObject camera = Instantiate(cameraPrefab, cameraPosition, new Quaternion(180, 0, 180, 0), cameraParent);
            camera.SetActive(false);
        }
    }

    // Reset the scene
    private void Reset()
    {
        yPosition = STANDARD_Y_POSITION;

        // Reset the 3D model
        for (int i = 0; i < modelParent.transform.childCount; i++)
        {
            Destroy(modelParent.transform.GetChild(i).gameObject);
            // Containers don't get destroyed
        }

        // Reset the input fields for submodules
        for (int i = 0; i < subModuleParent.transform.childCount; i++)
        {
            Destroy(subModuleParent.transform.GetChild(i).gameObject);
        }

        // Reset the input fields for camera's
        for (int i = 0; i < cameraFieldParent.transform.childCount; i++)
        {
            Destroy(cameraFieldParent.transform.GetChild(i).gameObject);
        }

        components.Clear();
    }

    // Setters for PLC connection
    public void SetNetId()
    {
        plcController.SetNetId(netIdField.text);
    }

    public void SetPort()
    {
        plcController.SetPort(int.Parse(portField.text));
    }

}
