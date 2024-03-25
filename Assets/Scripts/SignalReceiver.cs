using System.Collections;
using System.Collections.Generic;
using M2MqttUnity;
using UnityEngine;

public class SignalReceiver : MonoBehaviour
{
    public GameObject capsule; 
    private WarehouseController warehouseController;

private void Start()
{
    // Find or reference the M2MqttUnityClient script
    M2MqttUnityClient mqttClient = FindObjectOfType<M2MqttUnityClient>();

    warehouseController = FindObjectOfType<WarehouseController>();

    // Subscribe to the MessageReceived event
    if (mqttClient != null)
    {
        mqttClient.MessageReceived += OnMessageReceived;
    }

    if (warehouseController != null)
    {
        Debug.Log("MoveXPosition script found: " + warehouseController.name);
    }
    else
    {
        Debug.LogWarning("MoveXPosition script not found!");
    }
}


    // Handle the received signals
    private void OnMessageReceived(string topic, byte[] message)
    {
        // Parse the received JSON message
        string jsonString = System.Text.Encoding.UTF8.GetString(message);
        MessageData data = JsonUtility.FromJson<MessageData>(jsonString);
        
        // Check if the received signal contains valid data
        if (data != null)
        {
            if (topic == "position")
            {
                if (data.move == "A1")
                {
                    if (warehouseController != null)
                    {
                        warehouseController.MoveObjectsToTargetsA();
                    }
                }
                else if (data.move == "B1")
                {
                    if (warehouseController != null)
                    {
                        warehouseController.MoveObjectsToTargetsB();
                    }
                }
                else if (data.move == "C1")
                {
                    if (warehouseController != null)
                    {
                        warehouseController.MoveObjectsToTargetsC();
                    }
                }
                else if (data.move == "base")
                {
                    if (warehouseController != null)
                    {
                        warehouseController.MoveObjectsToTargetsBase();
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Received invalid message data");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the GameObject is destroyed
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
