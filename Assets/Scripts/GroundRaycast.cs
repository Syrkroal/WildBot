using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRaycast : MonoBehaviour
{

    private bool hasHit = false;
    public LayerMask lm;

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1, lm) == true)
            hasHit = true;
        else
            hasHit = false;
    }

    public bool HasHit()
    {
        return (hasHit);
    }
}
