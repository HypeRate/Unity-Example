using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    Vector3 initPosition;
    Quaternion initRotation;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        initRotation = transform.rotation;
    }

    public void Reset()
    {
        transform.position = initPosition;
        transform.rotation = initRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.enabled = true;
        }

        MeshCollider mc = GetComponent<MeshCollider>();
        if (mc != null)
        {
            mc.enabled = true;
        }
    }
}
