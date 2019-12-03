﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int currentLife = 10;
    public int maxLife = 10;
    public int ammo = 100;
    public float point = 0;

    void Start()
    {
        point = 0;
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (collision.gameObject.GetComponent<ItemType>().itemType == ItemType.type.Ammo)
            {
                ammo += (int)collision.gameObject.GetComponent<ItemType>().rate;
            }
            if (collision.gameObject.GetComponent<ItemType>().itemType == ItemType.type.Health)
            {
                currentLife += (int)collision.gameObject.GetComponent<ItemType>().rate;
                if (currentLife > maxLife) currentLife = maxLife;
            }
            Destroy(collision.gameObject);
        }
    }

    public void hitPlayer(int damage)
    {
        currentLife -= damage;
        print(currentLife);
        if (currentLife <= 0)
            playerDead();
    }

    void playerDead()
    {
        print("u ded");
    }

    public int shootAmmo(int bullets)
    {
        if (bullets <= 0)
            return 0;
        bullets--;
        return bullets;
    }

    public void reload(int loaderSize, int bullets)
    {
        ammo -= (loaderSize - bullets);
    }
}