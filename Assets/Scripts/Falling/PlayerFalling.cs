using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MonoBehaviour
{
    public enum State {Falling, Walking };
    public State state;

    public float moveSpeed = 1;
    public float turnSpeed = 8;
    public float fallSpeed = 1;
    public float moveSpeedIncreaseIfTiltingForward = 1;
    public float fallSpeedIncreaseIfTiltingForward = 1;

    public LayerMask unlandable;

    public Transform body;
    [HideInInspector]
    public Transform landedPlace;
    public float takeOffStartHeight = 10;

    private void Start()
    {
        state = State.Falling;
    }

    private void Update()
    {
        if (state == State.Falling)
        {
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

        if (state == State.Walking) { 
            if (Input.GetKeyDown(KeyCode.Space)) {
                transform.position = landedPlace.transform.position + (Vector3.up * takeOffStartHeight);
                state = State.Falling;
                landedPlace.GetComponent<LandablePlace>().landable = false;
            }
        }
    }
}