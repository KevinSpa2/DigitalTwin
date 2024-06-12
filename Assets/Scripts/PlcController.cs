using UnityEngine;
using TwinCAT.Ads;
using System;

public class PlcController : MonoBehaviour
{
    public string netId;
    public int port;

    public WarehouseController warehouseController;

    private TcAdsClient adsClient;
    private int horizontalPositionHandle;
    private int verticalPositionHandle;
    private int startMovementHandle;
    private int cantileverPositionHandle;
    private int startCantileverMovementHandle;
    private int currentColorHandle;
    private int moveForwardHandle;
    private int moveBackwardHandle;

    private int previousHorizontalPosition;
    private int previousVerticalPosition;
    private bool previousStartCantileverMovement;
    private bool previousStartMovement;


    void Start()
    {
        warehouseController = FindObjectOfType<WarehouseController>();
        try
        {
            adsClient = new TcAdsClient();
            adsClient.Connect(netId, port); // Change to your PLC's address

            // Attempt to create variable handles and log success or failure
            horizontalPositionHandle = adsClient.CreateVariableHandle("MAIN.UnityData.HorizontalPosition");
            verticalPositionHandle = adsClient.CreateVariableHandle("MAIN.UnityData.VerticalPosition");
            startMovementHandle = adsClient.CreateVariableHandle("MAIN.UnityData.StartMovement");
            cantileverPositionHandle = adsClient.CreateVariableHandle("MAIN.UnityData.CantileverPosition");
            startCantileverMovementHandle = adsClient.CreateVariableHandle("MAIN.UnityData.StartCantileverMovement");
            currentColorHandle = adsClient.CreateVariableHandle("MAIN.UnityData.CurrentColor");
            moveForwardHandle = adsClient.CreateVariableHandle("MAIN.UnityData.MoveForward");
            moveBackwardHandle = adsClient.CreateVariableHandle("MAIN.UnityData.MoveBackward");
            Debug.Log("PLC connection and variable handles created successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error during initialization: " + ex.Message);
        }
    }

    int CreateVariableHandle(string variablePath)
    {
        try
        {
            int handle = adsClient.CreateVariableHandle(variablePath);
            Debug.Log($"Successfully created handle for {variablePath}");
            return handle;
        }
        catch (AdsErrorException adsEx)
        {
            Debug.LogError($"ADS Error creating handle for {variablePath}: {adsEx.ErrorCode}");
            throw;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error creating handle for {variablePath}: {ex.Message}");
            throw;
        }
    }

    void Update()
    {
        try
        {
            int horizontalPosition = (int)adsClient.ReadAny(horizontalPositionHandle, typeof(int));
            int verticalPosition = (int)adsClient.ReadAny(verticalPositionHandle, typeof(int));
            bool startMovement = (bool)adsClient.ReadAny(startMovementHandle, typeof(bool));
            int cantileverPosition = (int)adsClient.ReadAny(cantileverPositionHandle, typeof(int));
            bool startCantileverMovement = (bool)adsClient.ReadAny(startCantileverMovementHandle, typeof(bool));
            int currentColor = (int)adsClient.ReadAny(currentColorHandle, typeof(int));
            bool moveForward = (bool)adsClient.ReadAny(moveForwardHandle, typeof(bool));
            bool moveBackward = (bool)adsClient.ReadAny(moveBackwardHandle, typeof(bool));
            // bool testRespond = (bool)adsClient.ReadAny(testRespondHandle, typeof(bool));
            
            if (startMovement != previousStartMovement) {
                warehouseController.MoveTurmToTarget(horizontalPosition, () => {
                    warehouseController.MoveAuslegerToTarget(verticalPosition, () => {
                        warehouseController.MoveGreiferToTarget(startCantileverMovement, horizontalPosition, verticalPosition);
                    });
                });
            } else {
                if (horizontalPosition != previousHorizontalPosition || verticalPosition != previousVerticalPosition || startCantileverMovement != previousStartCantileverMovement) {
                    warehouseController.MoveTurmToTarget(horizontalPosition, () => {
                        warehouseController.MoveAuslegerToTarget(verticalPosition, () => {
                            warehouseController.MoveGreiferToTarget(startCantileverMovement, horizontalPosition, verticalPosition);
                        });
                    });
                }
            }


            if (horizontalPosition != previousHorizontalPosition)
            {                
                previousHorizontalPosition = horizontalPosition;
            }
            if (verticalPosition != previousVerticalPosition)
            {
                previousVerticalPosition = verticalPosition;
            }
            if (startCantileverMovement != previousStartCantileverMovement)
            {
                previousStartCantileverMovement = startCantileverMovement;
            }
            if (startMovement != previousStartMovement)
            {
                previousStartMovement = startMovement;
            }

        }
        catch (AdsErrorException adsEx)
        {
            Debug.LogError("ADS Error: " + adsEx.ErrorCode);
        }
        catch (Exception ex)
        {
            Debug.LogError("PLC read error: " + ex.Message);
        }
    }

    void OnDestroy()
    {
        try
        {
            adsClient.Dispose();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error disposing ADS client: " + ex.Message);
        }
    }
}
