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
    public bool outOfWind;
    public GameObject windGust;

    bool inTrigger = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<BoxCollider>();
        controller = FindObjectOfType<vThirdPersonInput>();
        messagePopUp = FindObjectOfType<MessagePopUp>();
        if (gameManager.landings  == 2) {
            outOfWind = true;
            windGust.SetActive(false);
        }


    }
    private void Update()
    {
        if (inTrigger) {

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
    }

    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
        //Show text "press space to jump off"
        if (outOfWind)
        {
            messagePopUp.PopMessage("There is no wind left to carry me up.", false, 2f);
        }
        else
        {
            controller.canJump = true;
            messagePopUp.PopMessage("Press space to jump off.", false, 2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        controller.canJump = false;
    }
}
