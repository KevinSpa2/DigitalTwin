using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBeltManager : MonoBehaviour
{
    public WarehouseController warehouseController;
    public ConveyorBeltMovement conveyorBelt;
    ConveyorBeltMovement[] conveyorBelts;

    public void startup() 
    {
        warehouseController = FindObjectOfType<WarehouseController>();
        conveyorBelts = FindObjectsOfType<ConveyorBeltMovement>();
    }

    public void findBelt()
    {
        ConveyorBeltMovement foundBelt = null;

        foreach (ConveyorBeltMovement belt in conveyorBelts)
        {
            if (belt.onBelt)
            {
                foundBelt = belt;
                break; 
            }
        }

        if (foundBelt != conveyorBelt)
        {
            conveyorBelt = foundBelt;
        }
    }

    public void checkMovement(bool moveForward, bool previousMoveForward, bool moveBackward, bool previousMoveBackward)
    {
        if (conveyorBelt != null) 
        {
            if (moveForward && moveForward != previousMoveForward) 
            {
                conveyorBelt.MoveForward();
            }
            else if (moveBackward && moveBackward != previousMoveBackward) 
            {
                conveyorBelt.MoveBackward();
            }
        }
    }

    public void moveContainerForwards()
    {
        findBelt();
        if (conveyorBelt != null)
        {
            conveyorBelt.MoveForward();
        } 
    }

    public void moveContainerBackwards()
    {
        findBelt();
        if (conveyorBelt != null)
        {
            conveyorBelt.MoveBackward();
        } 
    }
}
