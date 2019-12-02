﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovement : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody rigidBody;
    private int direction = 1;
    public Transform player;
    private Transform myTransform;
    public float range = 5;
    public float attackRange = 1;
    public float minHeight = 6;
    public float rotationSpeed = 0.2f;
    private EnemyAttack attack;
    private Vector3 initialPos;
    private float rotStep;
    private float moveStep;
    private Animator anim;
    private bool animationIsPlaying = false;
    public float minBarrelRollRate = 2.5f;
    public float maxBarrelRollRate = 5f;
    private float nextRoll;
    private Transform childTransform;
    private EnemyHealth health;

    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        attack = GetComponent<EnemyAttack>();
        myTransform = transform;
        initialPos = myTransform.position;
        moveStep = speed * Time.deltaTime;
        anim = GetComponentInChildren<Animator>();
        health = GetComponentInChildren<EnemyHealth>();
        childTransform = transform.GetChild(0);
        rotStep = rotationSpeed * Time.deltaTime;
        if (!player)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }
    void FixedUpdate() {
        if (health.healthPoints <= 0)
            Destroy(gameObject);
        else
            AIMovement();

    }

    private IEnumerator PlayAnimation(string animName)
    {
        anim.Play(animName, 0);
        yield return new WaitForEndOfFrame();
        animationIsPlaying = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        animationIsPlaying = false;
    }

    private void ReturnToPos() {
        if (myTransform.position != initialPos) {
            Vector3 direction = initialPos - myTransform.position;
            Vector3 newRot = Vector3.RotateTowards(myTransform.forward, direction, rotStep, 0.0f);
            if (myTransform.rotation == Quaternion.LookRotation(newRot)) {
                myTransform.position = Vector3.MoveTowards(myTransform.position, initialPos, moveStep);
            } else {
                myTransform.rotation = Quaternion.LookRotation(newRot);
            }
        }
    }

    private void AIMovement () {
        if (Time.time > nextRoll)
        {
            float randTime = Random.Range(minBarrelRollRate, maxBarrelRollRate);
            nextRoll = Time.time + randTime;
            StartCoroutine(PlayAnimation("BarrelRoll"));
        }
        float distance = Vector3.Distance(myTransform.position, player.position);
        int playerDirection = (int)(player.position.x - myTransform.position.x);
        if (distance <= range) {
            Vector3 direction = player.position - myTransform.position;
            RaycastHit hit;
            bool hasHit = Physics.Raycast(myTransform.position, direction, out hit, 100);
            if (hasHit) {
                if (hit.transform.position == player.position) {
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(direction), rotStep);
                    // Vector3 newRot = Vector3.RotateTowards(myTransform.forward, direction, rotStep, 0.0f);
                    // myTransform.rotation = Quaternion.LookRotation(newRot);
                    Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
                    if (distance > attackRange) {
                        myTransform.position = Vector3.MoveTowards(myTransform.position, player.position, moveStep);
                        if (myTransform.position.y < minHeight)
                            myTransform.position = new Vector3(myTransform.position.x, minHeight, myTransform.position.z);
                    } else
                        attack.Fire(player.position - childTransform.position, childTransform.position);
                }
                else {
                    Debug.DrawRay(transform.position, direction * 50, Color.blue);
                    // ReturnToPos();
                }
            }
        } else
            ReturnToPos();
    }
}
