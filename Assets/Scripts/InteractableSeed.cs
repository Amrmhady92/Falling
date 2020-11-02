using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSeed : InteractableObject
{

    public IntField seedsCount;
    public GameValues gameValues;
    public float descaleTime = 1;
    internal override void Start()
    {
        base.Start();
        if (seedsCount == null) seedsCount = Resources.Load<IntField>("SeedsCount");
        if (seedsCount == null) Debug.LogError("No Intfield for SeedsCount");

        if (gameValues == null) gameValues = Resources.Load<GameValues>("GameValues");
        if (gameValues == null) Debug.LogError("No Game Values file found");
    }
    public override void Interact(GameObject player = null)
    {
        base.Interact();
        interactable = false;
        outliner.enabled = false;
        highlightable = false;
        if (seedsCount != null) seedsCount.Value++;
        if (player != null)
        {
            player.GetComponent<Animator>().Play("PickUp");
            //Player.Instance.StopMovementForATime(2.5f);
            if (gameValues != null) {
                string msg = gameValues.GetSeedMessage();
                player.GetComponent<MessagePopUp>().PopMessage(msg, msg != "", 3);
            }
        }
        //transform.LeanScale(Vector3.zero, descaleTime).setOnComplete(() => { this.gameObject.SetActive(false); });
    }

}
