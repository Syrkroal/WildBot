using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawner : MonoBehaviour
{

    public GameObject slight;

    public void SpawnLight()
    {
        Instantiate(slight, transform.position, new Quaternion(0, 0, 0, 0));
    }
}
