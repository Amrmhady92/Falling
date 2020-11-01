using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 targetOffset;
    public Transform target;
    public Transform lookTarget;
    Vector3 smoothVelocity;
    public float smoothMoveTime = 0.5f;

    Vector3 smoothLookVelocity;
    public float smoothLookMoveTime = 0.5f;

    void Start()
    {
        targetOffset = transform.position - target.position;

    }

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + targetOffset, ref smoothVelocity, smoothMoveTime);
        Debug.DrawLine(transform.position, transform.position + transform.forward * 20, Color.red);

        transform.LookAt(lookTarget);

    }
}

