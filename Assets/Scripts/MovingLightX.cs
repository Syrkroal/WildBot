using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLightX : MonoBehaviour
{
    public float speed;
    public float direction = -1;

    void Start()
    {
        Destroy(this.gameObject, 4f);
    }

    void Update()
    {
        transform.Translate(new Vector3(direction * speed * Time.deltaTime, 0 , 0));
    }

    public void dir(float angle)
    {
        if (angle == 90)
            direction = 1;
        else
            direction = -1;
    }
}
