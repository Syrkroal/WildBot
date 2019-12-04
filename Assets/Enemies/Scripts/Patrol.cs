﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private GroundEnemyMovement movement;
    public Transform player;
    private Transform myTransform;
    public float range = 5;
    public float attackRange = 1;
    private EnemyAttack attack;
    private BoxCollider boxCollider;
    public float rotationSpeed = 0.2f;
    private float rotStep;
    private bool animationIsPlaying = false;
    private Animator anim;
    private EnemyStat stat;

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        movement = GetComponent<GroundEnemyMovement>();
        attack = GetComponent<EnemyAttack>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        stat = GetComponent<EnemyStat>();
        myTransform = transform;
        agent.autoBraking = false;
        rotStep = rotationSpeed * Time.deltaTime;

        GotoNextPoint();
    }

    private IEnumerator PlayAnimation(string animName) {
        anim.Play(animName, 0);
        yield return new WaitForEndOfFrame();
        agent.isStopped = true;
        animationIsPlaying = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        animationIsPlaying = false;
        agent.isStopped = false;
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForEndOfFrame();
        agent.isStopped = true;
        animationIsPlaying = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        animationIsPlaying = false;
        agent.isStopped = false;
    }

    private void GotoNextPoint() {
        StartCoroutine(PlayAnimation("Idle"));
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
        agent.isStopped = false;
    }

    void Update() {
        if (stat.deathPlaying) {
            agent.isStopped = true;
        }
        if (!animationIsPlaying && !stat.deathPlaying) {

            Vector3 centerPos = myTransform.position + new Vector3(0, boxCollider.center.y, 0);
            float distance = Vector3.Distance(centerPos, player.position);

            if (distance <= range)
            {
                if (distance < attackRange)
                {
                    agent.isStopped = true;
                    Vector3 direction = player.position - centerPos;
                    Vector3 newRot = Vector3.RotateTowards(myTransform.forward, direction, rotStep, 0.0f);
                    float angle = Vector3.Angle(myTransform.forward, new Vector3(direction.x, 0 , 0));
                    if (angle < 15)
                    {
                        attack.MeleeAttack(player.position - centerPos, centerPos);
                        StartCoroutine(WaitForAnimation());
                    }
                    else
                    {
                        myTransform.rotation = Quaternion.LookRotation(newRot);
                    }
                }
                else {
                    agent.isStopped = false;
                    agent.SetDestination(player.position);
                }
            }
            else
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    GotoNextPoint();
            }
        }
    }
}
