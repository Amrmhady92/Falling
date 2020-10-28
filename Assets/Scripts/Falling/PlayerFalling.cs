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
    public Animator animator;
    public ParticleSystem diveWind;
    float originalEmission;

    private void Start()
    {
        originalEmission = diveWind.emission.rateOverTime.constant;
    }

    private void Update()
    {
        Vector3 inputHorizontal = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        Vector3 inputVertical = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
        float actualFallSpeed = fallSpeed;
        if (inputVertical.z > 0)
        {
            actualFallSpeed = fallSpeed + fallSpeedIncreaseIfTiltingForward;
            var diveEmission = diveWind.emission;
            diveEmission.rateOverTime = originalEmission * 2;
        }
        else
        {
            actualFallSpeed = fallSpeed;
            var diveEmission = diveWind.emission;
            diveEmission.rateOverTime = originalEmission;
        }

        transform.Rotate(transform.up, inputHorizontal.x);
        transform.Translate(Vector3.forward * (moveSpeed + (inputVertical.z * moveSpeedIncreaseIfTiltingForward)) * Time.deltaTime);
        animator.SetFloat("LeftRight", inputHorizontal.x, 1, Time.deltaTime);
        animator.SetFloat("Dive", inputVertical.z, 1f, Time.deltaTime);

        Ray downRay = new Ray(transform.position, Vector3.down);
        if (!Physics.Raycast(downRay, 3, unlandable))
        {
            transform.Translate(Vector3.down * actualFallSpeed * Time.deltaTime);
        }
        Ray collisionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(collisionRay, 4, unlandable))
        {
            FindObjectOfType<GustSpawner>().SpawnUpdraft();
        }

    }


}