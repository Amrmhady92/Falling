using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 targetOffset;
    public Transform target;
    public Transform lookTarget;
    Vector3 smoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        targetOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + targetOffset, ref smoothVelocity,  0.1f);
        Debug.DrawLine(transform.position, transform.forward * 20, Color.red);
        transform.LookAt(lookTarget);
    }
}

