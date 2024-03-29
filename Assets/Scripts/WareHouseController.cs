using System.Collections;
using UnityEngine;

public class WarehouseController : MonoBehaviour
{
    public Transform turm;
    public Transform ausleger;
    public Transform greifer;

    public float moveSpeed = 1.0f;

    public float minZPosition = 0f;
    public float maxZPosition = 2200f;

    public float minTurmPosition = 37.03607f;
    public float maxTurmPosition = 67.64607f;

    public float minAuslegerPosition = 34.17f;
    public float maxAuslegerPosition = 64.78f;

    public float minGreiferPosition = 29.43f;
    public float maxGreiferPosition = 60.04f;

    private bool isMoving = false; // Indicate if movement is in progress

    // Move objects to specified positions within the range of 0 to 2200
    public void MoveObjectsToTargets(float zPosition)
    {
        if (!isMoving)
        {
            float turmTarget = Map(zPosition, minZPosition, maxZPosition, minTurmPosition, maxTurmPosition);
            float auslegerTarget = Map(zPosition, minZPosition, maxZPosition, minAuslegerPosition, maxAuslegerPosition);
            float greiferTarget = Map(zPosition, minZPosition, maxZPosition, minGreiferPosition, maxGreiferPosition);

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

    private float Map(float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
}
