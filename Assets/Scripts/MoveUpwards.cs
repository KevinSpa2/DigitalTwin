using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpwards : MonoBehaviour
{
    public float speed = 1.0f; // Adjust the speed as needed

    private void Start()
    {
        // Start moving upwards when the game begins
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            // Move the object upwards
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            // Wait for the next frame
            yield return null;
        }
    }
}
