using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody rigidBody;
    public Transform player;
    private Transform myTransform;
    public float range = 5;
    public float attackRange = 1;
    public float minHeight = 5;
    public float maxHeight = 11;
    private float height;
    public float rotationSpeed = 1;
    private float rotStep;
    private float moveStep;
    private Animator anim;
    private bool hasHit = false;
    public float minBarrelRollRate = 2.5f;
    public float maxBarrelRollRate = 5f;
    private float nextHitCheck;
    private EnemyHealth health;
    private BoxCollider boxCollider;
    public LayerMask mLayerMask;
    private Vector3 stirDir;
    public Vector3 directionToPlayer;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
        moveStep = speed * Time.deltaTime;
        anim = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
        rotStep = rotationSpeed * Time.deltaTime;
        height = Random.Range(minHeight, maxHeight);
        if (!player)
            player = GameObject.FindWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        AIMovement();
    }

    private IEnumerator PlayAnimation(string animName)
    {
        anim.Play(animName, 0);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
    }

    private void CheckCollisionOverlap()
    {
        if (hasHit)
        {
            myTransform.Translate(stirDir * Time.deltaTime, Space.World);
            if (Time.time > nextHitCheck)
            {
                float randTime = Random.Range(minBarrelRollRate, maxBarrelRollRate);
                nextHitCheck = Time.time + randTime;
                hasHit = false;
            }
        }
        else
        {
            Collider[] hits = Physics.OverlapBox(myTransform.position, myTransform.localScale / 2, Quaternion.identity, mLayerMask);
            foreach (Collider hit in hits)
            {
                if (hit == boxCollider)
                    continue;
                hasHit = true;
                Vector3 closestPoint = hit.ClosestPointOnBounds(myTransform.position);
                Vector3 direction = myTransform.position - closestPoint;
                int randTime = Random.Range(0, 2);
                if (randTime == 0)
                    stirDir = Vector3.left;
                else
                    stirDir = Vector3.right;
            }
        }
    }

    private void AIMovement()
    {
        float distance = Vector3.Distance(myTransform.position, player.position);
        int playerDirection = (int)(player.position.x - myTransform.position.x);
        directionToPlayer = player.position - myTransform.position;
        if (health.deathPlaying)
        {
            rigidBody.useGravity = true;
            rigidBody.constraints = RigidbodyConstraints.None;
        }
        else if (distance <= range)
        {
            Vector3 direction = player.position - myTransform.position;
            RaycastHit hit;
            bool hasHit = Physics.Raycast(myTransform.position, direction, out hit, 100);
            if (hasHit)
            {
                if (hit.transform.position == player.position)
                {
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(direction), rotStep);
                    Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
                    if (distance > attackRange)
                    {
                        Vector3 playerPosMinHeight = new Vector3(player.position.x, height, player.position.z);
                        myTransform.position = Vector3.MoveTowards(myTransform.position, playerPosMinHeight, moveStep);
                    } else
                        myTransform.position = Vector3.MoveTowards(myTransform.position, player.position, moveStep);
                }
                else
                    Debug.DrawRay(transform.position, direction * 50, Color.blue);
            }
            CheckCollisionOverlap();
        }
    }
}

