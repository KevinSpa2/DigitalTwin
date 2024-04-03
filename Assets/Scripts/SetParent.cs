using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform);
    }

    public void ReleaseContainer(Transform transformComponent)
    {
        transformComponent.SetParent(null);
    }
}
