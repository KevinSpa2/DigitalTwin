# Digital Twin - High Bay Warehouse

## Getting Started

To run the digital twin environment, follow these steps:

1. **Clone or Download the Repository**: Clone this repository to your local machine, or download the files as a ZIP archive and extract them.

2. **Install unity:** Install Unity and open the project in editor version `2022.3.21f1` or above.

3. **Install an MQTT Broker**: Before running the digital twin, you need to have an MQTT broker installed locally. We recommend using  [Mosquitto](https://mosquitto.org/).

4. **Set Up MQTT Client**: To send messages to the digital twin, you need an MQTT client. You can use [MQTTX](https://mqttx.app/) or any other MQTT client application.

5. **Run the Digital Twin**: Run the Unity project by pressing 'play'.

6. **Send MQTT data:** Send the MQTT data with the MQTTX app as explained below.



## Sending Messages

Use the MQTTX client to send messages to the digital twin. You will need to connect to the local MQTT broker (default 127.0.0.1) and publish messages to ```'position'``` topic. 

###### Data format (json):

```json
{
  "moveTurm": "A",
  "moveAusleger": "1",
  "moveGripper": "r"
}
```

###### Available data to send:

```json
moveTurm:
    - "A" or "a": Right column
    - "B" or "b": Middle column
    - "C" or "c": Left column
    - "Base" or "base": Right
    Value between:
        - "0" (Start, Right) 
        - "2200" (End, Left)

moveAusleger:
    - "1": Top level
    - "2": Mid level
    - "3": Bottom level
    Value between:
    - "4" - Bottom
    - "1000" - Top

moveGripper:
    - "R", "r", "Rack", or "rack": Rack position
    - "A", "a", "Assembly", or "assembly": Assembly position


```



The data `moveTurm` and `moveAusleger` will relocate the entire tower to the correct position. By adding the `moveGripper` function, it will extend the gripper to either release or pick up a container, and then retract the gripper back to its default position.
