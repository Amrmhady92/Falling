using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchScript : MonoBehaviour
{
    [Tooltip("Where the camera will be positioned")]
    public Vector3 cameraTargetPosition;
    [Tooltip("The rotation the camera will have")]
    public Vector3 cameraTargetRotation;
    [Tooltip("Where the camera will be aimaing at, leave at 0,0,0 if not used")]
    public Vector3 cameraTargetLookAt; //Leave empty if not used
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Camera.main.transform.position = cameraTargetPosition;
        Camera.main.transform.eulerAngles = cameraTargetRotation;
        if (cameraTargetLookAt != new Vector3(0, 0, 0))
            Camera.main.transform.LookAt(cameraTargetLookAt);
    }
}
