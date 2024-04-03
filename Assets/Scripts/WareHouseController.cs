using System.Collections;
using UnityEngine;

public class WarehouseController : MonoBehaviour
{
    public Transform turm;
    public Transform ausleger;
    public Transform greifer;

    // Movement speed of the objects
    public float moveSpeed = 1.0f;

    public float minZPosition = 0f;
    public float maxZPosition = 2200f;

    public float minYPosition = 0f;
    public float maxYPosition = 1000f;

    public float minXPosition = 0f;
    public float maxXPosition = 100f;

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


    // Bools to track ongoing movement on Z and Y axes
    private bool isZMoving = false; 
    private bool isYMoving = false;
    private bool isXMoving = false;
    
    private bool movedDown;
    private bool firstXRun = true;

    // Move objects to specified positions within the range of 0 to 2200
    public void MoveObjectsToTargets(string moveZInstruction, System.Action onZMovementComplete)
    {
        float zPosition;

        // Check if moveInstruction is  numeric 
        if (float.TryParse(moveZInstruction, out zPosition))
        {
            // If moveInstruction is a numeric value, clamp it in the range of 0 and 2200
            zPosition = Mathf.Clamp(zPosition, minZPosition, maxZPosition);
        }
        // Predefined locations
        else
        {
            switch (moveZInstruction)
            {
                case "A":
                    zPosition = 810f;
                    break;
                case "B":
                    zPosition = 1455f;
                    break;
                case "C":
                    zPosition = 2100f;
                    break;
                case "base":
                    zPosition = 0f;
                    break;
                default:
                    zPosition = 0f;
                    break;
            }
        }

        if (!isZMoving)
        {
            float turmTarget = Map(zPosition, minZPosition, maxZPosition, minTurmZPosition, maxTurmZPosition);
            float auslegerTarget = Map(zPosition, minZPosition, maxZPosition, minAuslegerZPosition, maxAuslegerZPosition);
            float greiferTarget = Map(zPosition, minZPosition, maxZPosition, minGreiferZPosition, maxGreiferZPosition);

            // Move objects on Z-axis with coroutines
            StartCoroutine(MoveZAxis(turm, turmTarget));
            StartCoroutine(MoveZAxis(ausleger, auslegerTarget));
            StartCoroutine(MoveZAxis(greifer, greiferTarget, onZMovementComplete));
        }
    }
    
    public void MoveObjectToY(string moveYInstruction, System.Action onYMovementComplete)
    {
        float yPosition;

        // Check if moveInstruction is  numeric 
        if (float.TryParse(moveYInstruction, out yPosition) && yPosition > 3)
        {
            // If moveInstruction is a numeric value, clamp it in the range of 0 and 1000
            yPosition = Mathf.Clamp(yPosition, minYPosition, maxYPosition);
        }
        else
        {
            switch (moveYInstruction)
            {
                case "1":
                    yPosition = 935f;
                    break;
                case "2":
                    yPosition = 520f;
                    break;
                case "3":
                    yPosition = 110f;
                    break;
                default:
                    yPosition = 0f;
                    break;
            }
        }

        if (!isYMoving)
        {
            float auslegerTarget = Map(yPosition, minYPosition, maxYPosition, minAuslegerYPosition, maxAuslegerYPosition);
            float greiferTarget = Map(yPosition, minYPosition, maxYPosition, minGreiferYPosition, maxGreiferYPosition);

            StartCoroutine(MoveYAxis(ausleger, auslegerTarget));
            StartCoroutine(MoveYAxis(greifer, greiferTarget, onYMovementComplete));
        }
    }

    public void MoveObjectToX(string moveXInstruction)
    {
        float xPosition;

        switch (moveXInstruction)
        {
            case "R":
            case "r":
            case "rack":
            case "Rack":
                xPosition = maxGreiferXPosition;
                break;
            case "A":
            case "a":
            case "assembly":
            case "Assembly":
                xPosition = maxGreiferXPosition - 0.3f;
                break;
            default:
                xPosition = minGreiferXPosition;
                break;
        }

        // If moveInstruction is a numeric value, map it to the range defined by minXPosition and maxXPosition
        if (!isXMoving)
        {
            StartCoroutine(MoveXAxis(greifer, xPosition));
        }
        
    }



    // Coroutine to move object to target position on Z-axis
    private IEnumerator MoveZAxis(Transform target, float targetZ, System.Action onComplete = null)
    {
        isZMoving = true;

        while (Mathf.Abs(target.position.z - targetZ) > 0.01f)
        {
            float step = moveSpeed * Time.deltaTime;
            Vector3 currentPosition = target.position;
            Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y, targetZ);
            target.position = Vector3.MoveTowards(currentPosition, targetPosition, step);
            yield return null;
        }

        isZMoving = false;

        onComplete?.Invoke();
    }

    // Coroutine to move object to target position on Y-axis
    private IEnumerator MoveYAxis(Transform target, float targetY, System.Action onComplete = null)
    {
        isYMoving = true;

        while (Mathf.Abs(target.position.y - targetY) > 0.01f)
        {
            float step = moveSpeed * Time.deltaTime;
            Vector3 currentPosition = target.position;
            Vector3 targetPosition = new Vector3(currentPosition.x, targetY, currentPosition.z);
            target.position = Vector3.MoveTowards(currentPosition, targetPosition, step);
            yield return null;
        }

        isYMoving = false;

        onComplete?.Invoke();
    }
        
    private IEnumerator MoveXAxis(Transform target, float targetX)
    {
        isXMoving = true;

        while (Mathf.Abs(target.position.x - targetX) > 0.01f)
        {
            float step = moveSpeed * Time.deltaTime;
            Vector3 currentPosition = target.position;
            Vector3 targetPosition = new Vector3(targetX, currentPosition.y, currentPosition.z);
            target.position = Vector3.MoveTowards(currentPosition, targetPosition, step);
            yield return null;
        }

        isXMoving = false;    
        
        yield return new WaitForSeconds(0.3f);

        StartCoroutine(MoveYAxisSmooth(ausleger, ausleger.position - new Vector3(0f, 1f, 0f)));
        StartCoroutine(MoveYAxisSmooth(greifer, greifer.position - new Vector3(0f, 1f, 0f)));

        while (isYMoving)
        {
            yield return null;
        }


        // After reaching the target position, move back to minGreiferXPosition
        if (targetX != minGreiferXPosition)
        {
            StartCoroutine(MoveXAxis(target, minGreiferXPosition));
        }
    }

    private IEnumerator MoveYAxisSmooth(Transform target, Vector3 targetPosition)
    {
        isYMoving = true;

        while (Vector3.Distance(target.position, targetPosition) > 0.01f)
        {
            float step = moveSpeed * Time.deltaTime;
            target.position = Vector3.MoveTowards(target.position, targetPosition, step);
            yield return null;
        }

        isYMoving = false;
    }

    // Map a value from one range to another
    private float Map(float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
}
