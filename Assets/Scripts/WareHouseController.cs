using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class WarehouseController : MonoBehaviour
{
    // Components
    public Transform turm;
    public Transform ausleger;
    public Transform greifer;

    // Holding item
    public bool holdingItem = false;

    // Movement speed of the objects
    public float moveSpeed = 1.0f;

    // Shelf heights
    public float topLevel = 915f;
    public float midLevel = 520f;
    public float bottomLevel = 110f;

    // Min and max inputs
    public float minZPosition = 0f;
    public float maxZPosition = 2200f;

    public float minYPosition = 0f;
    public float maxYPosition = 1000f;


    // Z-axis position constraints for individual objects
    public float minTurmZPosition = 37.03607f;
    public float maxTurmZPosition = 67.64607f;

    public float minAuslegerZPosition = 34.17f;
    public float maxAuslegerZPosition = 64.78f;

    public float minGreiferZPosition = 29.43f;
    public float maxGreiferZPosition = 60.04f;

    // Y-axis position constraints for individual objects
    public float minAuslegerYPosition = 6.101268f;
    public float maxAuslegerYPosition = 20.64827f;

    public float minGreiferYPosition = 6.857f;
    public float maxGreiferYPosition = 21.404f;

    // X-axis position constrains for objects
    public float minGreiferXPosition = -11.17033f;
    public float maxGreiferXPosition = -7.4f;


    // Bools to track ongoing movement on all axis
    public bool isZMoving = false; 
    public bool isYMoving = false;
    public bool isXMoving = false;
    
    public bool correctTurmLocation = false;
    public bool correctAuslegerLocation = false;

    public MovingObjects movingObjects;
    public ShelfManager shelfManager;

    

    private void Start()
    {
        movingObjects = FindObjectOfType<MovingObjects>();
        shelfManager = FindObjectOfType<ShelfManager>();
    } 

    // Move objects to specified positions within the range of 0 to 2200
    public void MoveTurmToTarget(int moveZInstruction, System.Action onZMovementComplete)
    {
        float zPosition;

        // Check if moveInstruction is  numeric 
        if (moveZInstruction > 3 && moveZInstruction < 2200)
        {
            // If moveInstruction is a numeric value, clamp it in the range of 0 and 2200
            zPosition = moveZInstruction;
            correctTurmLocation = false;
        }
        // Predefined locations
        else
        {
            switch (moveZInstruction)
            {
                case 1:
                    zPosition = 810f;
                    correctTurmLocation = true;
                    break;
                case 2:
                    zPosition = 1455f;
                    correctTurmLocation = true;
                    break;
                case 3:
                    zPosition = 2100f;
                    correctTurmLocation = true;
                    break;
                case 0:
                    zPosition = 0f;
                    correctTurmLocation = true;
                    break;
                default:
                    zPosition = 0f;
                    correctTurmLocation = false;
                    break;
            }
        }

        if (!isZMoving)
        {
            float turmTarget = Map(zPosition, minZPosition, maxZPosition, minTurmZPosition, maxTurmZPosition);
            float auslegerTarget = Map(zPosition, minZPosition, maxZPosition, minAuslegerZPosition, maxAuslegerZPosition);
            float greiferTarget = Map(zPosition, minZPosition, maxZPosition, minGreiferZPosition, maxGreiferZPosition);

            // Move objects on Z-axis with coroutines
            StartCoroutine(movingObjects.MoveTurm(turm, turmTarget));
            StartCoroutine(movingObjects.MoveTurm(ausleger, auslegerTarget));
            StartCoroutine(movingObjects.MoveTurm(greifer, greiferTarget, onZMovementComplete));
        }
    }
    
    public void MoveAuslegerToTarget(int moveYInstruction, System.Action onYMovementComplete)
    {
        float yPosition;

        // Check if moveInstruction is  numeric 
        if (moveYInstruction > 3 && moveYInstruction < 1000)
        {
            // If moveInstruction is a numeric value, clamp it in the range of 0 and 1000
            yPosition = moveYInstruction;
        }
        else
        {
            switch (moveYInstruction)
            {
                case 1:
                    yPosition = topLevel;
                    correctAuslegerLocation = true;
                    break;
                case 2:
                    yPosition = midLevel;
                    correctAuslegerLocation = true;
                    break;
                case 3:
                    yPosition = bottomLevel;
                    correctAuslegerLocation = true;
                    break;
                default:
                    yPosition = 0f;
                    correctAuslegerLocation = false;
                    break;
            }
        }

        if (!isYMoving)
        {
            float auslegerTarget = Map(yPosition, minYPosition, maxYPosition, minAuslegerYPosition, maxAuslegerYPosition);
            float greiferTarget = Map(yPosition, minYPosition, maxYPosition, minGreiferYPosition, maxGreiferYPosition);

            StartCoroutine(movingObjects.MoveAusleger(ausleger, auslegerTarget));
            StartCoroutine(movingObjects.MoveAusleger(greifer, greiferTarget, onYMovementComplete));
        }
    }

    public void MoveGreiferToTarget(bool startPickUp, int column, int level)
    {
        float xPosition = maxGreiferXPosition; 
        
        Debug.Log(column+level);

        // If moveInstruction is a numeric value, map it to the range defined by minXPosition and maxXPosition
        if (!isXMoving && startPickUp && correctAuslegerLocation && correctTurmLocation)
        {
            if (shelfManager.SearchValue(column, level) && holdingItem)
            {
                Debug.Log("Shelf already occupied!");
            }
            else
            {
                StartCoroutine(movingObjects.MoveGreifer(greifer, xPosition));
            }
        }
    }

    // Map a value from one range to another
    private float Map(float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
}
