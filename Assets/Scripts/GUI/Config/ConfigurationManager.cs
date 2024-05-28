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
    private Transform modelParent;

    [SerializeField]
    private TMP_Text selectedFileText;

    void Awake(){
        if(modelParent.childCount > 0){
            GameObject model = modelParent.GetChild(0).gameObject;
            selectedFileText.text = "Huidig 3D-model: " + model.name;
        }
        else{
            selectedFileText.text = "Geen bestand geselecteerd";
        }
    }

}
