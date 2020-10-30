using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scanner : MonoBehaviour
{

    public GameObject coneObject;
    public bool scanning = false;
    public Vector3 coneFinalScale = new Vector3(1, 1, 1);
    public float rotationStartAngle = -1;
    public float rotationEndAngle = -60;
    public float coneRotationTime = 2.5f;
    public float coneScaleTime = 0.5f;

    public GameValues gameValues;
    public float coneCoolDown = 3.5f;
    public float animationTimeHandUp = 2;
    public bool ScanArea(Action onComplete)
    {
        if (scanning) return false;
        scanning = true;

        StartCoroutine(WaitThenDo(animationTimeHandUp, () => 
        {
            coneObject.transform.localScale = Vector3.zero;
            coneObject.transform.localEulerAngles = new Vector3(coneObject.transform.localEulerAngles.x, coneObject.transform.localEulerAngles.y, rotationStartAngle);

            coneObject.transform.LeanScale(coneFinalScale, coneScaleTime).setOnComplete(() =>
            {

                coneObject.LeanValue(rotationStartAngle, rotationEndAngle, coneRotationTime).setOnUpdate((float v) =>
                {
                    coneObject.transform.localEulerAngles =
                    new Vector3(coneObject.transform.localEulerAngles.x,
                                coneObject.transform.localEulerAngles.y,
                                v);
                }
                ).setOnComplete(() =>
                {
                    coneObject.transform.LeanScale(Vector3.zero, 0.1f);

                    if (this.gameValues != null)
                    {
                        Debug.Log("Popping message");
                        string msg = this.gameValues.GetGroundStatus();
                        Player.Instance.GetComponent<MessagePopUp>().PopMessage(msg, msg != "", 3);
                    }

                    this.StopAllCoroutines();
                    this.StartCoroutine(WaitThenDo(coneCoolDown, () =>
                    {
                        scanning = false;
                        onComplete?.Invoke();
                    }));
                //coneObject.SetActive(false);
                //coneObject.transform.localScale = Vector3.zero;
                
                });
            });
        }));
        return true;

        //coneObject.SetActive(true);

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
