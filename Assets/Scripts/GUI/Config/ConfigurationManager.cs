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
    private Transform modelParent, modelOptionsParent;

    [SerializeField]
    private List<GameObject> models;

    public void SelectModel(int index)
    {
        for(int i = 0; i < modelParent.transform.childCount; i++){
            Destroy(modelParent.transform.GetChild(i).gameObject);     
        }   

        GameObject selectedModel = Instantiate(models[index]);
        selectedModel.transform.SetParent(modelParent);
    }

}
