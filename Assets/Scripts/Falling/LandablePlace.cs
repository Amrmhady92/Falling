using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandablePlace : MonoBehaviour
{
    public string placeName;
    public Transform place;
    float coolDownTimer = 20;
    public bool landable = true;

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
            GameManager.instance.LoadNewScene(this);

            /*other.transform.position = place.transform.position + Vector3.up;
            other.GetComponent<PlayerFalling>().state = PlayerFalling.State.Walking;
            other.GetComponent<PlayerFalling>().landedPlace = transform;*/
        }
    }
}
