using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraVeritcal : MonoBehaviour
{
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public float mouseSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            yaw += mouseSpeed * Input.GetAxis("Mouse X");
            pitch -= mouseSpeed * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0.0f);
    }
}
