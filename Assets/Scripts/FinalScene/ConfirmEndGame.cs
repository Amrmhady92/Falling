using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmEndGame : MonoBehaviour
{
    public InteractablePlantingGround plantingGround;
    MessagePopUp messagePopUp;

    private void Start()
    {
        messagePopUp = FindObjectOfType<MessagePopUp>();

    }
    
    private void OnTriggerEnter(Collider other)
    {
        messagePopUp.PopMessage("press E to confirm planting", true, plantingGround.timeToDecideToPlant);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            StartCoroutine(plantingGround.ConfirmPlanting());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        messagePopUp.CancelMessage(false);
    }
}
