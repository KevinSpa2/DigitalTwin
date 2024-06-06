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
    private Transform modelParent, modelOptionsParent, subModuleParent;

    [SerializeField]
    private List<GameObject> models;

    [SerializeField]
    private GameObject subModuleFieldPrefab;

    private string inputText;
    
    private List<GameObject> subModules;

    // THIS LIST IS SUPPOSED TO BE USED IN STATUSBARCONTROLLER
    public static List<string> components = new List<string>();

    // Generate all necessary elements
    public void SelectModel(int index)
    {
        for(int i = 0; i < modelParent.transform.childCount; i++){
            Destroy(modelParent.transform.GetChild(i).gameObject);
            // DOESNT DESTROY CONTAINERS
        }   

        GameObject selectedModel = Instantiate(models[index]);
        selectedModel.transform.SetParent(modelParent);

        List<GameObject> children = new List<GameObject>();
        for(int i = 0; i < selectedModel.transform.childCount; i++){
            GameObject child = selectedModel.transform.GetChild(i).gameObject;
            children.Add(child);
            components.Add(child.name);
        }

        int yPosition = 625;
        for(int i = 0; i < selectedModel.transform.childCount; i++){
            // CAN ADD THESE INFINITELY BY PRESSING THE BUTTON AGAIN, NEED TO BE DESTROYED FIRST ON CLICK
            GameObject subModule = Instantiate(subModuleFieldPrefab, new Vector3(1237, yPosition, 0), new Quaternion(0, 0, 0, 0), subModuleParent);
            subModule.GetComponent<TMP_InputField>().text = components[i];
            subModules.Add(subModule);
            yPosition -= 85;
        }
    }

    // Save input in inputfields
    public void SaveSubModuleNames(){
        // ON END EDIT & ON DESELECT
        for(int i = 0; i < modelParent.transform.childCount; i++){
            components[i] = subModules[i].GetComponent<TMP_InputField>().text;
        }
    }

}
