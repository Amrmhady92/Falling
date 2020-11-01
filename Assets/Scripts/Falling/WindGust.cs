using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGust : MonoBehaviour
{
    public float gustSpeed;
    public Vector3 gustDirection;
    public float timeToGustFullStrenght = .25f;
    Vector3 windVelocity;

    private void Start()
    {
        windVelocity = Vector3.zero;
        if (gustDirection == Vector3.zero)
        {
            gustDirection = Vector3.up;
        }
        if (gustDirection.magnitude > 1)
        {
            gustDirection = gustDirection.normalized;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        windVelocity = Vector3.Lerp(windVelocity, gustDirection * gustSpeed, timeToGustFullStrenght *Time.fixedDeltaTime);
        other.GetComponent<Rigidbody>().AddForce(windVelocity, ForceMode.Impulse);
    }
}
