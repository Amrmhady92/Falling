using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public float minDistance = 2;
    public GameObject target;
    void Update()
    {
        if(target != null)
        {
            if(Vector3.Distance(this.transform.position, target.transform.position) > minDistance)
            {
                this.transform.LookAt(target.transform);
                //this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
            }
        }
    }
}
