using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public int m_seeds = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == "Flower")
        {
            Destroy(collision.gameObject);
            Debug.Log("Seeds received");
            m_seeds++;
        }
    }
}

