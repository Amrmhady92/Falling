using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmEndGame : MonoBehaviour
{
    public InteractablePlantingGround plantingGround;
    MessagePopUp messagePopUp;
    bool inTrigger = false;

    private void Start()
    {
        messagePopUp = FindObjectOfType<MessagePopUp>();

    }

    private void Update()
    {
        if (inTrigger) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(plantingGround.ConfirmPlanting());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
        messagePopUp.PopMessage("press E to confirm planting", true, plantingGround.timeToDecideToPlant);
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        messagePopUp.CancelMessage(false);
        gameObject.SetActive(false);
    }
}
