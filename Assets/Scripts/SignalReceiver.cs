using System.Collections;
using System.Collections.Generic;
using M2MqttUnity;
using UnityEngine;

public class SignalReceiver : MonoBehaviour
{
    public WarehouseController warehouseController;

    private void Start()
    {
        M2MqttUnityClient mqttClient = FindObjectOfType<M2MqttUnityClient>();

        warehouseController = FindObjectOfType<WarehouseController>();

        if (mqttClient != null)
        {
            mqttClient.MessageReceived += OnMessageReceived;
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
                    warehouseController.MoveObjectsToTargets(data.moveZ, () =>
                    {
                        
                        // Executes when Z-axis movement completes
                        warehouseController.MoveObjectToY(data.moveY, () =>
                        {
                            warehouseController.MoveObjectToX(data.moveX);
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
        public string moveZ;
        public string moveY;
        public string moveX;
    }
}
