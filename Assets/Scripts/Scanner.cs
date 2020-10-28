using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scanner : MonoBehaviour
{

    public GameObject coneObject;
    public bool scanning = false;
    public Vector3 coneFinalScale = new Vector3(1, 1, 1);
    public float coneScaleTime = 3;
    public float coneCoolDown = 2;

    public bool ScanArea(Action onComplete)
    {
        if (scanning) return false;
        scanning = true;

        //coneObject.SetActive(true);
        coneObject.transform.localScale = Vector3.zero;

        coneObject.transform.LeanScale(coneFinalScale, coneScaleTime).setOnComplete(() =>
        {
            StartCoroutine(WaitThenDo(coneCoolDown, () => { scanning = false; }));
            //coneObject.SetActive(false);
            //coneObject.transform.localScale = Vector3.zero;
            coneObject.transform.LeanScale(Vector3.zero, 0.2f);
            onComplete?.Invoke();
        });

        return true;
    }

    IEnumerator WaitThenDo(float wait, Action callbackDo)
    {
        yield return new WaitForSeconds(wait);
        callbackDo?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!scanning) return;

        Debug.Log("Hits");
        InteractableObject interactable = other.GetComponent<InteractableObject>();
        if(interactable == null) interactable = other.GetComponentInChildren<InteractableObject>();
        if (interactable != null) interactable.Highlight(true);
    }


}
