using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectile;
    public bool canDeal;
    public float damage;
    public List<AudioClip> sounds = new List<AudioClip>();

    private float fireRate = 2;
    private float nextFire;
    private float meleeAttackRate = 2;
    private float nextMeleeAttack;
    private Animator anim;
    private Patrol patrol;
    private AudioSource sound;

    void Start() {
        anim = GetComponent<Animator>();
        patrol = GetComponent<Patrol>();
        sound = GetComponent<AudioSource>();
    }

    public void Fire(Vector3 direction, Vector3 spawnPos) {
        if (Time.time > nextFire)
        {
            sound.PlayOneShot(sounds[1]);
            nextFire = Time.time + fireRate;
            GameObject go = Instantiate(projectile, spawnPos, Quaternion.identity);
            go.GetComponent<Projectile>().setDirection(direction);
        }
    }

    public void MeleeAttack()
    {
        if (Time.time > nextMeleeAttack)
        {
            sound.PlayOneShot(sounds[0]);
            canDeal = true;
            anim.Play("Attack", 0);
            patrol.isAttacking = true;
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
