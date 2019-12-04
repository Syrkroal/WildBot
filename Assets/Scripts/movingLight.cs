using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingLight : MonoBehaviour
{
    public float speed = 10;
    private Vector3 direction = new Vector3(0, 0, -1);
    private Light lightRef;
    public GameObject ball;
    private float timer = 0;
    private bool goBall = false;
    Vector3 scale = new Vector3(1, 1, 1);
    public GameObject SpherePrefabs;
    private GameObject SphereTmp; 

    void Start()
    {
        lightRef = GetComponent<Light>();
        Destroy(this.gameObject, 3);
        SphereTmp = Instantiate(SpherePrefabs, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(SphereTmp, 3);
    }

    void Update()
    {
        if (transform.position.z > -100)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            lightRef.intensity = transform.position.z / -50;
            timer = Time.time + 0f;
           SphereTmp.transform.localScale = new Vector3(lightRef.intensity * 20, lightRef.intensity * 20, lightRef.intensity * 20);
        }
        else if (goBall == true)
        {
            ball.SetActive(true);
            scale += new Vector3(speed / 5 * Time.deltaTime, speed / 5   * Time.deltaTime, 0);
            ball.transform.localScale = scale;
        }
        else if (goBall == false && timer <= Time.time)
        {
            goBall = true;
        }
    }

}
