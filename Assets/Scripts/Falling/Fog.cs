using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public int xPos;
    public int yPos;
    ParticleSystem pSystem;

    private void Start()
    {

    }

    public void RemoveFog() {
        var thisEmission = GetComponent<ParticleSystem>().emission;
        thisEmission.rateOverTime = 0;
        Destroy(gameObject, 3);
    }

    public void SetPosValues(int x, int y) {
        xPos = x;
        yPos = y;
    }
}
