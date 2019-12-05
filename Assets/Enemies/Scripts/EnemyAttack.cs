using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectile;
    public bool canDeal;
    public float damage;
    private float fireRate = 2;
    private float nextFire;
    private float meleeAttackRate = 2;
    private float nextMeleeAttack;
    private Animator anim;
    private Patrol patrol;

    void Start() {
        anim = GetComponent<Animator>();
        patrol = GetComponent<Patrol>();
    }

    public void Fire(Vector3 direction, Vector3 spawnPos) {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject go = Instantiate(projectile, spawnPos, Quaternion.identity);
            go.GetComponent<Projectile>().setDirection(direction);
        }
    }

    public void MeleeAttack()
    {
        if (Time.time > nextMeleeAttack)
        {
            canDeal = true;
            anim.Play("Attack", 0);
            patrol.isAttacking = true;
            nextMeleeAttack = Time.time + meleeAttackRate;
        }
    }
}
