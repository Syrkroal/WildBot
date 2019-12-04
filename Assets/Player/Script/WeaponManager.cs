﻿using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject projectile;
    public GameObject weapon;
    public GameObject parentWeapon;
    private GameObject Player;
    public Transform recoilStart;
    public Transform recoilBack;
    public Transform NormalShoot;
    public Transform PreciseShoot;

    public float fireRate = 0.2f;
    private float nextFire = 0;
    public float recoilSpeed = 1f;
    public float recoilLaps = 0.05f;
    public int loaderSize = 20;
    public float damage = 10.0f;
    public float headshotMultiplier = 2.5f;
    private int bulletInLoader;

    public float lockViewSpeed = 1.5f;

    void Awake()
    {
        bulletInLoader = loaderSize;
        damage = 10.0f;
        print(damage);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire(transform.forward, weapon.transform.position);
        }
        if (Time.time < (nextFire + recoilLaps - fireRate))
            doRecoil();
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.GetComponent<PlayerManager>().reload(loaderSize, bulletInLoader);
            bulletInLoader = loaderSize;
        }
        //replaceWeapon();
        
        if (Input.GetMouseButton(1))
        {
            GotoPreciseView();
        }
/*        else
            GotoNormalView();*/
    }


    public void Fire(Vector3 direction, Vector3 spawnPos)
    {
        if (Time.time > nextFire)
        {
            bulletInLoader = transform.GetComponent<PlayerManager>().shootAmmo(bulletInLoader);
            if (bulletInLoader > 0)
            {
                nextFire = Time.time + fireRate;
                GameObject go = Instantiate(projectile, spawnPos, Quaternion.identity);
                go.GetComponent<ProjectilePlayer>().setDirection(direction);
            }
        }
    }

    public void doRecoil()
    {
        print("wp pos :");
        print(weapon.transform.position);
        print("going to :");
        print(recoilBack.position);
        weapon.transform.position = Vector3.MoveTowards(weapon.transform.position, recoilBack.position, Time.deltaTime * recoilSpeed);
    }

    public void replaceWeapon()
    {
        weapon.transform.position = Vector3.MoveTowards(weapon.transform.position, recoilStart.position, Time.deltaTime * recoilSpeed / 2);
    }

    public void GotoPreciseView()
    {
        parentWeapon.transform.position = Vector3.MoveTowards(parentWeapon.transform.position, PreciseShoot.position, Time.deltaTime * lockViewSpeed);
    }

    public void GotoNormalView()
    {
        parentWeapon.transform.position = Vector3.MoveTowards(parentWeapon.transform.position, NormalShoot.position, Time.deltaTime * lockViewSpeed);
    }
}