using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandablePlace : MonoBehaviour
{
    public string placeName;
    float coolDownTimer = 20;
    public bool landable = true;
    public Transform takeOffPoint;

    private void Update()
    {
        if (!landable) {
            if (coolDownTimer > 0) {
                coolDownTimer -= Time.deltaTime;
            } else {
                landable = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (landable)
        {
            StartCoroutine(GameManager.instance.LoadNewScenes(this, .5f));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(takeOffPoint.transform.position, 1);
    }
}
