using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyAttack : MonoBehaviour
{
    public int damage = 1;
    private float fireRate = 2;
    private float nextFire;
    private Follow follow;

    void Start()
    {
        follow = GetComponent<Follow>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                PlayerManager player = other.gameObject.GetComponent<PlayerManager>();
                player.hitPlayer(damage);
                player.Bump(follow.directionToPlayer);
            }
        }
    }
}
