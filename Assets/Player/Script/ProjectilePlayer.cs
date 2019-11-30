﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    public float speed = 1;
    public float timer = 2;
    private float damage;
    public GameObject DisplayDamage;
    private Rigidbody rigidBody;
    private GameObject Owner;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, timer);
        Owner = GameObject.FindGameObjectWithTag("Player");
        damage = Owner.GetComponent<WeaponManager>().damage;
    }

    public void setDirection(Vector3 direction) {
        transform.rotation = Quaternion.LookRotation(direction);
    }
    void Update()
    {
        rigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth)
                enemyHealth.TakeDamage(damage);

            GameObject damageText = Instantiate(DisplayDamage, transform.position, Quaternion.identity);
            //damageText.GetComponent<DamageDisplay>().set;
            Destroy(gameObject);
        }
    }
}
