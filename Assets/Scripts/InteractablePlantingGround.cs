using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlantingGround : InteractableObject
{

    public GameObject billboard;
    public Vector3 billboardStartPosition;
    public Vector3 billboardEndScale;
    public float billboardHeight = 2;
    public bool useCenterAsPosition = false;
    public float riseUpTime = 1;

    public GameObject confirmer;
    public float timeToDecideToPlant = 5f;
    
    internal override void Start()
    {
        base.Start();
        if (billboard == null) Debug.LogError("No BillBoard on land patch");
        else
        {
            billboard.transform.localScale = Vector3.zero;
            if (useCenterAsPosition)
            {
                billboard.transform.position = this.transform.position;
            }
            else
            {
                billboard.transform.position = billboardStartPosition;
            }

        }
        confirmer.SetActive(false);
    }
    public override void Highlight(bool onOff)
    {
        //Activated through Scanner
        //No need to check Hightlightable
        if (detected) return;
        if (billboard == null) return;

        detected = true;

        billboard.transform.localScale = Vector3.forward; // need only x and y to be zero

        if (useCenterAsPosition)
        {
            billboard.transform.position = this.transform.position;
        }
        else
        {
            billboard.transform.position = billboardStartPosition;
        }


        billboard.transform.LeanScale(billboardEndScale, riseUpTime);
        billboard.transform.LeanMove(billboard.transform.position + Vector3.up * billboardHeight, riseUpTime);

    }

    public override void Interact(GameObject player = null)
    {
        base.Interact();
        interactable = false;
        //if (player != null) player.GetComponent<Animator>().Play("PickUp");
        Debug.Log("Interacted with ground");

        confirmer.SetActive(true);
        Invoke("TurnOffConfirmer", timeToDecideToPlant);
    }

    public void ConfirmPlanting() {

        StartCoroutine(GameManager.instance.LoadFinalScene());

    }

    void TurnOffConfirmer() {
        confirmer.SetActive(false);
        interactable = true;
    }
}
