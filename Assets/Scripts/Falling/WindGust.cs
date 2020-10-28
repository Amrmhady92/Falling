using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGust : MonoBehaviour
{
    public float gustSpeed;
    public bool scriptedDirection;
    public Vector2 gustSpeedMinMax;
    public Vector3 gustDirection;



    private void Start()
    {
        gustSpeed = Random.Range(gustSpeedMinMax.x, gustSpeedMinMax.y);
        if (!scriptedDirection)
        {
            gustDirection = new Vector3(Random.Range(-1, 1), Random.Range(0, 1), Random.Range(-1, 1));
            if (gustDirection == Vector3.zero) {
                gustDirection = Vector3.up;
            }
        }
        if (gustDirection.magnitude > 1)
        {
            gustDirection = gustDirection.normalized;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        MoveTransform(other.transform);
    }

    void MoveTransform(Transform playerT)
    {
        playerT.Translate(gustDirection * gustSpeed * Time.deltaTime, Space.World);
    }


}
