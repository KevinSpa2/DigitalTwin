using System.Collections;
using System.Collections.Generic;
using M2MqttUnity;
using UnityEngine;
using UnityEngine.UI;

public class SignalReceiver : MonoBehaviour
{
    public WarehouseController warehouseController;

    [SerializeField]
    private Toggle toggle;

    private void Start()
    {
        // toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnSwitch);
        if (toggle.isOn)
        {
            OnSwitch(toggle.isOn);
        }
    }
    private void OnSwitch(bool isOn)
    {
        if (!isOn)
        {
            M2MqttUnityClient mqttClient = FindObjectOfType<M2MqttUnityClient>();
            warehouseController = FindObjectOfType<WarehouseController>();

            if (mqttClient != null)
            {
                mqttClient.MessageReceived += OnMessageReceived;
            }
            else
            {
                Debug.LogWarning("Could not find M2MqttUnityClient script!");
            }

            if (warehouseController != null)
            {
                Debug.Log("Found warehouse script: " + warehouseController.name);
            }
            else
            {
                Debug.LogWarning("Could not find WarehouseController script!");
            }
        }
    }

    private void OnMessageReceived(string topic, byte[] message)
    {
        string jsonString = System.Text.Encoding.UTF8.GetString(message);
        MessageData data = JsonUtility.FromJson<MessageData>(jsonString);

        if (data != null)
        {
            if (topic == "position")
            {
                if (warehouseController != null)
                {
                    warehouseController.MoveTurmToTarget(data.moveTurm, () =>
                    {
                        
                        // Executes when Z-axis movement completes
                        warehouseController.MoveAuslegerToTarget(data.moveAusleger, () =>
                        {
                            warehouseController.MoveGreiferToTarget(data.moveGripper, data.moveTurm, data.moveAusleger);
                        });
                    });
                }
            }
        }
        else
        {
            Debug.LogWarning("Received invalid data");
        }
    }

    private void OnDestroy()
    {
        M2MqttUnityClient mqttClient = FindObjectOfType<M2MqttUnityClient>();
        if (mqttClient != null)
        {
            mqttClient.MessageReceived -= OnMessageReceived;
        }
    }

    [System.Serializable]
    public class MessageData
    {
        public int moveTurm;
        public int moveAusleger;
        public bool moveGripper;
    }
}
