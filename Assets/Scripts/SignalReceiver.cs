using System.Collections;
using System.Collections.Generic;
using M2MqttUnity;
using UnityEngine;
using UnityEngine.UI;

public class SignalReceiver : MonoBehaviour
{
    public WarehouseController warehouseController;
    public M2MqttUnityClient mqttClient;

    [SerializeField]
    private GameObject toggleObject;

    void Awake()
    {
        Toggle toggle = toggleObject.GetComponent<Toggle>();
        if (toggle == null)
        {
            Debug.LogError("Toggle component niet gevonden!");
            return;
        }

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
        {
            OnSwitch(toggle.isOn);
        }
    }

    private void OnSwitch(bool isOn)
    {
        if (isOn)
        {
            if (mqttClient != null)
            {
                mqttClient.MessageReceived -= OnMessageReceived;
            }
        }
        else
        {
            if (mqttClient != null)
            {
                mqttClient.MessageReceived += OnMessageReceived;
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
