using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounding : MonoBehaviour
{
    public List<LandablePlace> landablePlaces;
    Transform nearestPlace;

    public float gustSpeed;
    float startGustSpeed;
    public Vector3 gustDirection;

    private void Start()
    {
        startGustSpeed = gustSpeed;
        landablePlaces = new List<LandablePlace>(FindObjectsOfType<LandablePlace>());
        float minDistance = float.MaxValue;
        foreach (LandablePlace place in landablePlaces) {
            float distance = Vector3.Distance(transform.position, place.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                nearestPlace = place.transform;
            }

        }
        gustDirection = (nearestPlace.position - transform.position).normalized;

    }

    /*private void Update()
    {
        Debug.DrawRay(transform.position, gustDirection, Color.red, 100);
    }*/

    private void OnTriggerStay(Collider other)
    {
        MoveTransform(other.transform);
    }

    void MoveTransform(Transform playerT)
    {
        playerT.Translate(gustDirection * gustSpeed * Time.deltaTime, Space.World);

    }

    private void OnTriggerExit(Collider other)
    {
        gustSpeed = startGustSpeed;
    }
}
