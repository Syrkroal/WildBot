using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject projectile;
    public GameObject weapon;
    public Transform recoilStart;
    public Transform recoilBack;

    public float fireRate = 0.2f;
    private float nextFire = 0;
    public float recoilSpeed = 1f;
    public float recoilLaps = 0.05f;
    public int loaderSize = 20;
    private int bulletInLoader;

    void Start()
    {
        bulletInLoader = loaderSize;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire(transform.forward, transform.position);
        }
        if (Time.time < (nextFire + recoilLaps - fireRate))
            doRecoil();
        replaceWeapon();
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
        weapon.transform.position = Vector3.MoveTowards(weapon.transform.position, recoilBack.position, Time.deltaTime * recoilSpeed);
    }

    public void replaceWeapon()
    {
        weapon.transform.position = Vector3.MoveTowards(weapon.transform.position, recoilStart.position, Time.deltaTime * recoilSpeed / 2);
    }
}