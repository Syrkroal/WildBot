using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectile;
    private float fireRate = 2;
    private float nextFire;
    private float meleeAttackRate = 2;
    private float nextMeleeAttack;
    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void Fire(Vector3 direction, Vector3 spawnPos) {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject go = Instantiate(projectile, spawnPos, Quaternion.identity);
            go.GetComponent<Projectile>().setDirection(direction);
        }
    }

    public void MeleeAttack(Vector3 direction, Vector3 spawnPos)
    {
        if (Time.time > nextMeleeAttack)
        {
            anim.Play("Attack", 0);
            nextMeleeAttack = Time.time + meleeAttackRate;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerHealth stat = collider.gameObject.GetComponent<PlayerHealth>();
            if (stat)
            {
                stat.TakeDamage(1);
            }
        }
    }
}
