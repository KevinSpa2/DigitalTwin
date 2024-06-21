# Digital Twin - High Bay Warehouse

## For use
This section contains a description of how to use the digital twin application.

### Requirements
- A windows, linux or mac device

### Installation & Start-up
- Download FTFDigitalTwin.zip from the repository
- Extract all files from the zip
- Run FTFDigitalTwin.exe

These steps will open the application. From there it can be used as described in the user guide document.

## For contribution
This section contains information for following projects.

### Requirements
- [Unity version 2022.3.21f1](https://unity.com/download)
- [A Realvirtual.io Professional license](https://realvirtual.io/en/buy/)
- All files in this repository

### Start-up
- Clone all files in this repository
- Install Realvirtual
- Open the project in Unity
- Import Realvirtual into the project by navigating to Assets->Import Package->Custom Package and select the realvirtual.unitypackage file.

After following the mentioned steps, the project should started up correctly.

### File structure
All necessary files for adjusting the project can be found in the 'Assets'-folder.

- 'Gizmos' is a Realvirtual-related folder containing icons used by the Realvirtual asset
- 'Models' is used for the various 3D-models which can be placed in the scene
- 'Prefab' is a folder that is used for all Unity prefabs
- 'Scenes' contains all scenes for the application (currently only 1 scene)
- 'Scripts' contains all scripts that the application uses
- 'Sprites' is used for all images used by the app
- 'TextMesh Pro' is a folder for the free TextMeshPro asset which is used by the GUI. If this asset isn't installed, Unity will ask first to install this asset.

### Scene structure
The 'DigitalTwinScene' in Unity is currently set up in a way which should make adding additional functionalities easier.
- 'Main Camera' and 'Directional Light' are standard in a Unity scene and are used for the visual aspect of the scene.
- '3DModel' is an empty GameObject which serves as a parent for the currently loaded 3D-model.
- 'Cameras' is an empty GameObject which should hold all cameras (aside from main) in the scene. The cameras for the around view are already in this object.
- 'Containers' is an empty GameObject which holds all containers loaded in the scene.
- 'Canvas' is a Canvas-object which contains all graphical user interface components.
- 'EventSystem' allows for the digital twin to receive input through buttons or input fields for example.
- 'StatusBarManager' is a GameObject that holds the scripts related to the status bar.
- 'CameraManager' is a GameObject that holds the scripts related to the cameras.
- 'ConfigurationManager' is a GameObject that holds the scripts related to the configuration screen.
- 'ControllerScript' is a GameObject that holds the scripts related to the behavioural controller for the 3D-model.
- 'realvirtual' is a prefab from the Realvirtual asset, which is required for the interface to work.
- 'TwinCATInterface' is a prefab from the Realvirtual asset, which is required to obtain PLC-data through the ADS-protocol.
- 'PLCInterface' is a GameObject that holds the scripts related to connecting the digital twin to the PLC.

### Current functionalities
To learn about the complete descriptions of the current functionalities of the digital twin, we will redirect you towards the design document.

- Receive and handle PLC-data for following the physical FTF
- Controller in the GUI for independent control
- Toggle to switch between follow-mode and independent control
- Status bar for monitoring the activities of the digital twin
- Camera controls for better viewing of the digital twin
- Customizable configuration

All functionalities have been tested using a virtual PLC. For learning how to set up a virtual PLC, we will redirect you towards your projectleader.

### Next steps
On top of the current functionalities, more can be added to the Digital Twin.

#### Testing with physical FTF
In the current state, the digital twin has not been tested with the PLC of the physical FTF. This is a necessary step to determine whether the digital twin is a 1-to-1 copy of the FTF. Factors that need to be tested are:
- Movement speed of the FTF. This can be adjusted in the digital twin by changing the 'Move Speed' value in the WareHouseController-script in the 'ControllerScript'-object in the scene.
- Testing behaviour. This consists of behaviour when receiving incorrect PLC data, or checking if the "Turm" and "Ausleger" can move simultaneously or not.

#### Additional factory modules
The digital twin has been set up for the high bay warehouse of the FTF. There are another 3 modules, as well as the entire FTF, which can be implemented into the digital twin. Each module needs the following parts:
- A 3D model, which is rendered in the scene so the module is visible.
- A controllerscript, which contains all behaviour for that module.
- A controller in the GUI, which can be used for controlling the digital twin if it's not connected to the PLC.
- A connection with Realvirtual.

Additionally, the PLC commands need to be added in the PLC code. For this we redirect you to your project leader.

#### Modularity
For modularity, the configuration screen has been created. Currently only the high bay warehouse can be selected here. In a later project 3D-models for the other modules can be added.

Currently only the names of the submodules and the camera positions are customizable. In a later project, there should also be options to add a Controller-script here, as well as a GUI controller for each specific module.

It needs to be mentioned that customizing camera positions will create a camera for each submodule at the given position, but these cameras aren't connected to the submodule-buttons yet.