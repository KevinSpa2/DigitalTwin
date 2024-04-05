using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    private WarehouseController warehouseController;
    
    private void Start()
    {
        warehouseController = FindObjectOfType<WarehouseController>();
    }   

    // Move turm to target position on Z-axis
    public IEnumerator MoveTurm(Transform target, float targetTurm, System.Action onComplete = null)
    {
        warehouseController.isZMoving = true;

        while (Mathf.Abs(target.position.z - targetTurm) > 0.01f)
        {
            float step = warehouseController.moveSpeed * Time.deltaTime;
            Vector3 currentPosition = target.position;
            Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y, targetTurm);
            target.position = Vector3.MoveTowards(currentPosition, targetPosition, step);
            yield return null;
        }

        warehouseController.isZMoving = false;

        onComplete?.Invoke();
    }

    // Move ausleger to vertical target position on Y-axis
    public IEnumerator MoveAusleger(Transform target, float targetAusleger, System.Action onComplete = null)
    {
        warehouseController.isYMoving = true;
        if (!warehouseController.holdingItem) {
            targetAusleger = targetAusleger - 1f;
        }
        while (Mathf.Abs(target.position.y - targetAusleger) > 0.01f)
        {
            float step = warehouseController.moveSpeed * Time.deltaTime;
            Vector3 currentPosition = target.position;
            Vector3 targetPosition = new Vector3(currentPosition.x, targetAusleger, currentPosition.z);
            target.position = Vector3.MoveTowards(currentPosition, targetPosition, step);
            yield return null;
        }

        warehouseController.isYMoving = false;

        onComplete?.Invoke();
    }
    
    // Move greifer to target position on X-axis
    public IEnumerator MoveGreifer(Transform target, float targetGreifer, bool isExtended = false)
    {
        warehouseController.isXMoving = true;

        while (Mathf.Abs(target.position.x - targetGreifer) > 0.01f)
        {
            float step = warehouseController.moveSpeed * Time.deltaTime;
            Vector3 currentPosition = target.position;
            Vector3 targetPosition = new Vector3(targetGreifer, currentPosition.y, currentPosition.z);
            target.position = Vector3.MoveTowards(currentPosition, targetPosition, step);
            yield return null;

        }

        warehouseController.isXMoving = false;    
        
        if (!isExtended)
        {
            yield return new WaitForSeconds(0.3f);
            
            if (warehouseController.holdingItem)
            {
                StartCoroutine(MoveGreiferVertical(warehouseController.ausleger, warehouseController.ausleger.position - new Vector3(0f, 1f, 0f)));
                StartCoroutine(MoveGreiferVertical(warehouseController.greifer, warehouseController.greifer.position - new Vector3(0f, 1f, 0f)));
            }
            else
            {
                StartCoroutine(MoveGreiferVertical(warehouseController.ausleger, warehouseController.ausleger.position - new Vector3(0f, -1f, 0f)));
                StartCoroutine(MoveGreiferVertical(warehouseController.greifer, warehouseController.greifer.position - new Vector3(0f, -1f, 0f)));
            }


            while (warehouseController.isYMoving)
            {
                yield return null;
            }


            // After reaching the target position, move back to minGreiferXPosition
            if (targetGreifer != warehouseController.minGreiferXPosition)
            {
                StartCoroutine(MoveGreifer(target, warehouseController.minGreiferXPosition, true));
            }
        }
    }

    private IEnumerator MoveGreiferVertical(Transform target, Vector3 targetPosition)
    {
        warehouseController.isYMoving = true;

        while (Vector3.Distance(target.position, targetPosition) > 0.01f)
        {
            float step = warehouseController.moveSpeed * Time.deltaTime;
            target.position = Vector3.MoveTowards(target.position, targetPosition, step);
            yield return null;
        }

        warehouseController.isYMoving = false;
    }
}

