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

    public void SelectModel(int index)
    {
        for(int i = 0; i < modelParent.transform.childCount; i++){
            Destroy(modelParent.transform.GetChild(i).gameObject);
            // DOESNT DESTROY CONTAINERS
        }   

        GameObject selectedModel = Instantiate(models[index]);
        selectedModel.transform.SetParent(modelParent);

        int yPosition = 625;

        for(int j = 0; j < selectedModel.transform.childCount; j++){
            GameObject subModule = Instantiate(subModuleFieldPrefab, new Vector3(1237, yPosition, 0), new Quaternion(0, 0, 0, 0), subModuleParent);
            yPosition -= 85;
        }
    }

}
