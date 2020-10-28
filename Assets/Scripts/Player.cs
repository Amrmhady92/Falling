﻿using Invector.vCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    public IntField seedsCount;
    public float interactRadius = 1;
    Animator animator;
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
    private void Start()
    {

        Cursor.visible = false;
        controller = this.GetComponent<vThirdPersonController>();
        animator = this.GetComponent<Animator>();
        seedsCount.Value = 0;
        //seedsCount.OnValueChanged += OnValueChange;

    }
    private void Update()
    {

        if (!interacting)
        {
            foundInteractable = false;
            detectionSphereCenter = this.transform.position + this.transform.forward * interactRadius;
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
            nearestInteractable.Interact();
            animator.Play("PickUp");
            nearestInteractable = null;
            DisableControllers();
            StartCoroutine(WaitThenDo(1.5f, () => { EnableControllers(); interacting = false; }));
        }

        if (Input.GetKeyDown(KeyCode.R) && !interacting)
        {
            ScanArea();
        }

        
    }




    public void ScanArea()
    {
        if (!scanner.ScanArea(EnableControllers)) return;
        DisableControllers();
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
        Gizmos.DrawWireSphere(this.transform.position + this.transform.forward * interactRadius, interactRadius);
    }
}
