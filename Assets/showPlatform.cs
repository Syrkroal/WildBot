using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showPlatform : MonoBehaviour
{
    GameObject tmp = null;
    bool isctive = false;
    public string tag;

    void Update()
    {
        if (tmp == null)
        {
            isctive = false;
        }
        GetComponent<MeshRenderer>().enabled = isctive;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == tag)
        {
            isctive = true;
            tmp = other.gameObject;
        }
    }
}
