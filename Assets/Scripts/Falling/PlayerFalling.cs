using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MonoBehaviour
{

    public float moveSpeed = 1;
    public float turnSpeed = 8;
    public float fallSpeed = 1;
    public float moveSpeedIncreaseIfTiltingForward = 1;
    public float fallSpeedIncreaseIfTiltingForward = 1;

    public LayerMask unlandable;

    public Transform body;

    float timeSinceGust;
    float gustTimeThreshold;
    public Vector2 timeBetweenGustsMinMax;

    public WindGust gustPrefab;

    private void Start()
    {
        gustTimeThreshold = Random.Range(timeBetweenGustsMinMax.x, timeBetweenGustsMinMax.y);
    }

    private void Update()
    {

        timeSinceGust += Time.deltaTime;

        if (timeSinceGust >= gustTimeThreshold) {
            SpawnGust();
            gustTimeThreshold = Random.Range(timeBetweenGustsMinMax.x, timeBetweenGustsMinMax.y);
            timeSinceGust = 0;
        }

        Vector3 inputHorizontal = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
            Vector3 inputVertical = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
            float actualFallSpeed = fallSpeed;
            if (inputVertical.z > 0)
            {
                actualFallSpeed = fallSpeed + fallSpeedIncreaseIfTiltingForward;
            }
            else
            {
                actualFallSpeed = fallSpeed;
            }

            transform.Rotate(transform.up, inputHorizontal.x);
            transform.Translate(Vector3.forward * (moveSpeed + (inputVertical.z * moveSpeedIncreaseIfTiltingForward)) * Time.deltaTime);
            Ray ray = new Ray(transform.position, Vector3.down);
            if (!Physics.Raycast(ray, 3, unlandable))
            {
                transform.Translate(Vector3.down * actualFallSpeed * Time.deltaTime);
            }
        
    }

    void SpawnGust() {

        Vector3 spawnPoint = transform.position + (transform.forward * 4);
        WindGust newGust = Instantiate(gustPrefab, spawnPoint, Quaternion.identity);
        newGust.scriptedDirection = false;
        Destroy(newGust.gameObject, 5f);

    }
}