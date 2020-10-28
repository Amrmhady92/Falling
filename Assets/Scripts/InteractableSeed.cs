using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSeed : InteractableObject
{

    public IntField seedsCount;
    public float descaleTime = 1;
    internal override void Start()
    {
        base.Start();
        if (seedsCount == null) seedsCount = Resources.Load<IntField>("SeedsCount");
        if (seedsCount == null) Debug.LogError("No Intfield for SeedsCount");
    }
    public override void Interact()
    {
        base.Interact();
        interactable = false;
        outliner.enabled = false;
        highlightable = false;
        if (seedsCount != null) seedsCount.Value++;

        transform.LeanScale(Vector3.zero, descaleTime).setOnComplete(() => { this.gameObject.SetActive(false); });
    }

}
