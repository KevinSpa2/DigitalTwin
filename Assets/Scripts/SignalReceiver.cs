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
            Debug.Log("WarehouseController script gevonden: " + warehouseController.name);
        }
        else
        {
            Debug.LogWarning("WarehouseController script niet gevonden!");
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
                float zPosition;
                // Check if incomming data is numeric
                if (float.TryParse(data.move, out zPosition))
                {
                    if (warehouseController != null)
                    {
                        zPosition = Mathf.Clamp(zPosition, 0f, 2200f);
                        warehouseController.MoveObjectsToTargets(zPosition);
                    }
                }
                else
                {
                    switch (data.move)
                    {
                        case "A1":
                            zPosition = 810f; 
                            break;
                        case "B1":
                            zPosition = 1455f; 
                            break;
                        case "C1":
                            zPosition = 2100f; 
                            break;
                        case "base":
                            zPosition = 0f; 
                            break;
                        default:
                            zPosition = 0f; 
                            break;
                    }

                    if (warehouseController != null)
                    {
                        warehouseController.MoveObjectsToTargets(zPosition);
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Ontvangen ongeldige berichtgegevens");
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
        public string move;
    }
}
