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
    private GameObject modelOptionPrefab;

    [SerializeField]
    private List<GameObject> models;

    void Awake(){
        // foreach(model : models){
        //     Instantiate(modelOptionPrefab);
        // }
    }

    public void addModel(GameObject model){
        this.models.Add(model);
    }

}
