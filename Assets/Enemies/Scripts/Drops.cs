using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    public GameObject[] items;
    public float height = 2;
    private bool isQuitting;

    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    void OnDestroy() {
        if (!isQuitting) {
            for (int i = 0; i < items.Length; i++)
            {
                ItemType item = items[i].GetComponent<ItemType>();
                if (item)
                {
                    float rand = Random.Range(0, 100);
                    if (rand < item.rate)
                    {

                        if (transform.position.y < height)
                            Instantiate(items[i], new Vector3(transform.position.x, height, transform.position.z), Quaternion.identity);
                        else
                            Instantiate(items[i], transform.position, Quaternion.identity);
                    }
                }
            }
        }
    }
}
