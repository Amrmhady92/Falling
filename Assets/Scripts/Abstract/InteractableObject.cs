using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class InteractableObject : MonoBehaviour
{
    public bool interactable = true;
    public bool highlightable = true;

    internal Outline outliner;
    public Color detectedOutlineColor;
    public Color selectedOutlineColor;

    public float detectedOutlineWidth = 3;
    public float selectedOutlineWidth = 5;

    public float interactionAnimationTime = 2;
    public bool detected = false;


    internal virtual void Start()
    {
        Init();
    }
    internal virtual void Init()
    {
        outliner = this.GetComponent<Outline>();
        if (outliner == null)
        {
            highlightable = false;
        }
        else outliner.enabled = false;

    }

    public virtual void Interact(GameObject player = null) 
    {
        if (!interactable)
        {
            Debug.Log("Cant Interact : interactable = false");
            return;
        }
    }

    public virtual void Highlight(bool onOff)
    {
        if (!highlightable) return;
        outliner.enabled = onOff;
        detected = true;
    }

    public virtual void SetSelectedHighlight(bool onOff)
    {
        if (!highlightable) return;
        if (outliner.enabled && onOff)
        {
            outliner.OutlineColor = selectedOutlineColor;
            outliner.OutlineWidth = selectedOutlineWidth;
        }
        if (outliner.enabled && !onOff)
        {
            outliner.enabled = detected;
            outliner.OutlineColor = detectedOutlineColor;
            outliner.OutlineWidth = detectedOutlineWidth;
        }
        if (!outliner.enabled && onOff)
        {
            outliner.enabled = true;
            outliner.OutlineColor = selectedOutlineColor;
            outliner.OutlineWidth = selectedOutlineWidth;

        }

    }

}
