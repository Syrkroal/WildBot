using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.GetComponent<MovePlayer>().LastCheckpont = new Vector3(transform.position.x, transform.position.y, other.transform.position.z);
            other.GetComponent<MovePlayer>().nblight = 4;
        }
    }
}
