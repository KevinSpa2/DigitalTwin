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
    [SerializeField]
    private Transform modelParent, modelOptionsParent, subModuleParent, cameraFieldParent;

    [SerializeField]
    private List<GameObject> models;

    [SerializeField]
    private GameObject subModuleFieldPrefab, cameraFieldPrefab;

    private string inputText;
    private int yPosition;
    private List<GameObject> subModules, children;
    private GameObject selectedModel;

    // THIS LIST IS SUPPOSED TO BE USED IN STATUSBARCONTROLLER
    public static List<string> components = new List<string>();

    // Generate all necessary elements
    public void SelectModel(int index)
    {
        this.Reset();

        // Load the 3D model into the scene
        selectedModel = Instantiate(models[index], new Vector3(0, 11.02f, 0), Quaternion.identity, modelParent);

        // Find all children of the 3D model
        this.children = new List<GameObject>();
        for(int i = 0; i < selectedModel.transform.childCount; i++){
            GameObject child = selectedModel.transform.GetChild(i).gameObject;
            children.Add(child);
            components.Add(child.name);
        }

        // Generate the inputfields for the submodules
        yPosition = 550;
        this.subModules = new List<GameObject>();
        for(int i = 0; i < components.Count; i++){
            GameObject subModule = Instantiate(subModuleFieldPrefab, new Vector3(1200, yPosition, 0), Quaternion.identity, subModuleParent);
            subModule.GetComponent<TMP_InputField>().text = components[i];
            subModules.Add(subModule);
            yPosition -= 85;
        }
    }

    // Save input in inputfields
    public void SaveSubModuleNames(){
        for(int i = 0; i < selectedModel.transform.childCount; i++){
            components[i] = subModules[i].GetComponent<TMP_InputField>().text;
        }
        StatusBarController.Instance.SetStatusBarNames();
    }

    // Reset the 3D model in the scene
    private void Reset(){
        yPosition = 550;

        for(int i = 0; i < modelParent.transform.childCount; i++){
            Destroy(modelParent.transform.GetChild(i).gameObject);
            // DOESNT DESTROY CONTAINERS
        }

        for(int i = 0; i < subModuleParent.transform.childCount; i++){
            Destroy(subModuleParent.transform.GetChild(i).gameObject);
        }

        components.Clear();
    }

}
