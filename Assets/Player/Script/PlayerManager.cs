using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int life = 5;
    public int ammo = 100;

    void Start()
    {

    }

    void Update()
    {

    }

    public void hitPlayer(int damage)
    {
        life -= damage;
        print(life);
        if (life <= 0)
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