using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public float minDistance = 2;
    [SerializeField]Player player;
    void Update()
    {
        if(player != null)
        {
            if(Vector3.Distance(this.transform.position,player.transform.position) > minDistance)
            {
                this.transform.LookAt(player.transform);
                this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
            }
        }
    }
}
