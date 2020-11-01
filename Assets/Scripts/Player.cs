﻿using Invector.vCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    public IntField seedsCount;
    public GameValues gameValues;

    public float interactRadius = 1;
    public Animator animator;
    vThirdPersonController controller;
    [SerializeField] Scanner scanner;


    InteractableObject nearestInteractable;
    InteractableObject lastInteractable;

    Collider[] hits;
    InteractableObject hitObejct;
    Vector3 detectionSphereCenter;
    float maxDistance;
    bool interacting = false;
    bool foundInteractable = false;

    private static Player instance;

    public static Player Instance
    {
        get
        {
            return instance;
        }
    }

    private void Start()
    {

        Cursor.visible = false;
        controller = this.GetComponent<vThirdPersonController>();
        animator = this.GetComponent<Animator>();
        
        StartCoroutine(WaitThenDo(0.5f, () => { animator.Play("Land"); }));

        seedsCount.Value = 0;
        if (gameValues == null) gameValues = Resources.Load<GameValues>("GameValues");
        if (instance == null) instance = this;
        //seedsCount.OnValueChanged += OnValueChange;

    }
    private void Update()
    {

        if (!interacting)
        {
            foundInteractable = false;
            detectionSphereCenter = this.transform.position + this.transform.forward * interactRadius * 0.9f;
            hits = Physics.OverlapSphere(detectionSphereCenter, interactRadius);
            maxDistance = interactRadius * 2;
            if (nearestInteractable != null)
            {
                lastInteractable = nearestInteractable;
            }
            else lastInteractable = null;

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    hitObejct = hits[i].gameObject.GetComponent<InteractableObject>();
                    if (hitObejct != null)
                    {
                        foundInteractable = true;
                        if (Vector3.Distance(hitObejct.transform.position, this.transform.position) < maxDistance)
                        {
                            nearestInteractable = hitObejct;
                        }
                    }
                }
            }



            if (nearestInteractable != null && !foundInteractable)
            {
                nearestInteractable.SetSelectedHighlight(false);
                nearestInteractable = null;
            }


            if (nearestInteractable != null && nearestInteractable != lastInteractable)
            {
                if (lastInteractable != null) lastInteractable.SetSelectedHighlight(false);
                nearestInteractable.SetSelectedHighlight(true);
            }
        }


        if (Input.GetKeyDown(KeyCode.E) && nearestInteractable != null && !interacting)
        {
            interacting = true;
            nearestInteractable.Interact(this.gameObject);
            //animator.Play("PickUp");
            DisableControllers();
            StartCoroutine(WaitThenDo(nearestInteractable.interactionAnimationTime, () => { EnableControllers(); interacting = false; }));
            nearestInteractable = null;

        }

        if (Input.GetKeyDown(KeyCode.R) && !interacting)
        {
            ScanArea();
            
        }

        
    }


    public void StopMovementForATime(float time)
    {
        Debug.Log("Stopping");
        DisableControllers();
        StartCoroutine(WaitThenDo(time, EnableControllers));
    }
    


    public void ScanArea()
    {
        if (!scanner.ScanArea(EnableControllers)) return;
        DisableControllers();
        this.GetComponent<MessagePopUp>().PopMessage("Scanning Ground", false, 1.5f);
        animator.Play("Scan");
    }

    public void EnableControllers()
    {
        controller.lockMovement = false;
        controller.lockRotation = false;

    }

    public void DisableControllers()
    {
        controller.lockMovement = true;
        controller.lockRotation = true;
        controller.verticalSpeed = 0;
        controller.horizontalSpeed = 0;
        controller.inputMagnitude = 0;
        controller._rigidbody.velocity = Vector3.zero;
    }


    IEnumerator WaitThenDo(float wait, Action callbackDo)
    {
        yield return new WaitForSeconds(wait);
        callbackDo?.Invoke();
    }

    void OnValueChange(int i)
    {
        //Debug.Log("+1");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + this.transform.forward * interactRadius * 0.9f, interactRadius);
    }
}
