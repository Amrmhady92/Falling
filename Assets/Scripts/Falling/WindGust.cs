using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGust : MonoBehaviour
{
    public float gustSpeed;
    public Vector3 gustDirection;

    private void Start()
    {
        if (gustDirection != Vector3.zero) {
            gustDirection = gustDirection.normalized;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        MoveTransform(other.transform);
    }

    void MoveTransform(Transform playerT) {
        playerT.Translate(gustDirection * gustSpeed * Time.deltaTime, Space.World);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }*/

}
