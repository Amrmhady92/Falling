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
    public Animator rotationAnimator;

    public Rigidbody myRigidbody;
    Vector3 myVelocity;

    public ParticleSystem diveWind;
    float originalEmission;

    public float noWindDistanceThreshold;
    List<LandablePlace> landablePlaces;
    [HideInInspector]
    public bool withinDistanceThreshold;

    private void Start()
    {
        originalEmission = diveWind.emission.rateOverTime.constant;
        landablePlaces = new List<LandablePlace>(FindObjectsOfType<LandablePlace>());
    }

    private void Update()
    {
        StartCoroutine(DistanceToLandablePlaceCheck());
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        float actualFallSpeed = fallSpeed;

        if (input.z > 0)
        {
            actualFallSpeed =   Mathf.Lerp(fallSpeed, fallSpeed + fallSpeedIncreaseIfTiltingForward, .2f);
            SetEmission(true);
        }
        else
        {
            actualFallSpeed = fallSpeed;
            SetEmission(false);
        }

        transform.Rotate(transform.up, input.x);
        myVelocity = transform.forward * (moveSpeed + (input.z * moveSpeedIncreaseIfTiltingForward));

        //animator.SetFloat("LeftRight", input.x, 1, Time.deltaTime);
        animator.SetFloat("Dive", input.z, 1f, Time.deltaTime);
        rotationAnimator.SetFloat("LeftRight", input.x, 1, Time.deltaTime);
        rotationAnimator.SetFloat("Dive", input.z, 1f, Time.deltaTime);

        CheckIfCloseToHittingGround(actualFallSpeed);
        CheckIfCloseToHittingObject();
    }

    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + myVelocity * Time.fixedDeltaTime) ;
    }

    IEnumerator DistanceToLandablePlaceCheck() {

        foreach (LandablePlace place in landablePlaces) {
            Vector3 playerPosition2D = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 placePostion2D = new Vector3(place.transform.position.x, 0, place.transform.position.z);
            float sqrDist = (playerPosition2D - placePostion2D).sqrMagnitude;
            if (sqrDist < (noWindDistanceThreshold * noWindDistanceThreshold)){
                withinDistanceThreshold = true;
                break;
            }
            else {
                withinDistanceThreshold = false;
            }
        }

        yield return new WaitForSeconds(0.5f);
    
    }
    void CheckIfCloseToHittingGround(float actualFallSpeed) {
        Ray downRay = new Ray(transform.position, Vector3.down);
        if (!Physics.Raycast(downRay, 3, unlandable))
        {
            myVelocity.y -= actualFallSpeed;
        }

    }
    void CheckIfCloseToHittingObject() {
        Ray collisionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(collisionRay, 4, unlandable))
        {
            FindObjectOfType<GustSpawner>().SpawnUpdraft();
        }
    }

    void SetEmission(bool diving) {
        var diveEmission = diveWind.emission;
        if (diving) {
            diveEmission.rateOverTime = originalEmission * 2;
        }
        else {
            diveEmission.rateOverTime = originalEmission;
        }
    }
}