using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int currentLife = 10;
    public int maxLife = 10;

    public int ammo = 100;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        print("hmmmm");

        if (collision.gameObject.CompareTag("Item"))
        {
            print("ok");
            if (collision.gameObject.GetComponent<ItemType>().itemType == ItemType.type.Ammo)
            {
                ammo += (int)collision.gameObject.GetComponent<ItemType>().rate;
                print(collision.gameObject.GetComponent<ItemType>().rate);
            }
            if (collision.gameObject.GetComponent<ItemType>().itemType == ItemType.type.Health)
            {
                currentLife += (int)collision.gameObject.GetComponent<ItemType>().rate;
                if (currentLife > maxLife) currentLife = maxLife;
            }
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
        print(bullets);
        return bullets;
    }

    public void reload(int loaderSize, int bullets)
    {
        ammo -= (loaderSize - bullets);
        print(ammo);
    }
}