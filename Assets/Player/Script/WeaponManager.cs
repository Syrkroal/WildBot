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
    void Start()
    {
        
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
            nextFire = Time.time + fireRate;
            GameObject go = Instantiate(projectile, spawnPos, Quaternion.identity);
            go.GetComponent<ProjectilePlayer>().setDirection(direction);
        }
    }

    public void doRecoil()
    {
        print("ok");
        weapon.transform.position = Vector3.MoveTowards(weapon.transform.position, recoilBack.position, Time.deltaTime * recoilSpeed);
    }

    public void replaceWeapon()
    {
        print("-----------");

        weapon.transform.position = Vector3.MoveTowards(weapon.transform.position, recoilStart.position, Time.deltaTime * recoilSpeed / 2);
    }
}
