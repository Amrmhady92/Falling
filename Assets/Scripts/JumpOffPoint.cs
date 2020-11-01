using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class JumpOffPoint : MonoBehaviour
{
    public BoxCollider col;
    vThirdPersonInput controller;
    GameManager gameManager;
    MessagePopUp messagePopUp;
    bool returning = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<BoxCollider>();
        controller = FindObjectOfType<vThirdPersonInput>();
        messagePopUp = FindObjectOfType<MessagePopUp>();
                
    }

    private void OnTriggerEnter(Collider other)
    {
        //Show text "press space to jump off"
        controller.canJump = true;
        messagePopUp.PopMessage("press space to jump off", false, 2f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!returning)
            {
                print("HEY");
                StartCoroutine(gameManager.ReturnToMainScene());
                returning = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        controller.canJump = false;
    }
}
