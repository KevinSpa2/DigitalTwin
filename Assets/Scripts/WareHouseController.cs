using System.Collections;
using UnityEngine;

public class WarehouseController : MonoBehaviour
{
    public Transform turm;
    public Transform ausleger;
    public Transform greifer;

    public float moveSpeed = 1.0f; 

    private bool isMoving = false; // Indicate if movement is in progress

    // Target positions for objects
    public float turmTargetA = 48.28607f;
    public float auslegerTargetA = 45.42f;
    public float greiferTargetA = 40.68f;

    public float turmTargetB = 57.36607f;
    public float auslegerTargetB = 54.5f;
    public float greiferTargetB = 49.76f;

    public float turmTargetC = 66.35f;
    public float auslegerTargetC = 63.48393f;
    public float greiferTargetC = 58.74393f;

    public float turmTargetBase = 37.03607f;
    public float auslegerTargetBase = 34.17f;
    public float greiferTargetBase = 29.43f;

    // Move objects to specified positions
    public void MoveObjectsToTargets(float turmTarget, float auslegerTarget, float greiferTarget)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine(turm, turmTarget));
            StartCoroutine(MoveCoroutine(ausleger, auslegerTarget));
            StartCoroutine(MoveCoroutine(greifer, greiferTarget));
        }
    }

    // Coroutine to move object to target position
    private IEnumerator MoveCoroutine(Transform target, float targetZ)
    {
        isMoving = true;

        while (Mathf.Abs(target.position.z - targetZ) > 0.01f)
        {
            float step = moveSpeed * Time.deltaTime;
            Vector3 currentPosition = target.position;
            Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y, targetZ);
            target.position = Vector3.MoveTowards(currentPosition, targetPosition, step);
            yield return null;
        }

        isMoving = false;
    }

    // Moving objects to different targets
    public void MoveObjectsToTargetsA()
    {
        MoveObjectsToTargets(turmTargetA, auslegerTargetA, greiferTargetA);
    }

    public void MoveObjectsToTargetsB()
    {
        MoveObjectsToTargets(turmTargetB, auslegerTargetB, greiferTargetB);
    }

    public void MoveObjectsToTargetsC()
    {
        MoveObjectsToTargets(turmTargetC, auslegerTargetC, greiferTargetC);
    }

    public void MoveObjectsToTargetsBase()
    {
        MoveObjectsToTargets(turmTargetBase, auslegerTargetBase, greiferTargetBase);
    }
}
