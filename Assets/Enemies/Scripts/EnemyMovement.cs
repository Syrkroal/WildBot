using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 40;
    public bool flying = false;
    private Rigidbody rigidBody;
    private int direction = 1;
    // private Collider collider;
    private float distanceToGround;
    private bool didHit = false;
    private Transform player;
    private Transform myTransform;
    public float range = 5;
    public float attackRange = 1;
    public float stop = 2;
    public float rotationSpeed = 3;
    private EnemyAttack attack;
    private float fireRate = 2;
    private float nextFire;

    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        attack = GetComponent<EnemyAttack>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myTransform = transform;
    }
    void FixedUpdate() {
        AIMovement();
    }

    private void AIMovement () {
        RaycastHit hit;
        float distance = Vector3.Distance(myTransform.position, player.position);
        int playerDirection = (int)(player.position.x - myTransform.position.x);
        if (playerDirection != 0)
            playerDirection /= Mathf.Abs(playerDirection);
        bool hasHit = Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGround + 0.1f);
        if (hasHit && !didHit) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            didHit = true;
        } else if (!hasHit && didHit) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            direction *= -1;
            didHit = false;
        }
        // float xMove = (speed * Time.deltaTime) * direction; //speed = 200
        // Vector3 newVelocity = new Vector3(xMove, 0, 0);
        // rigidBody.velocity = newVelocity;
        if (distance <= range) {
            //look
            // myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
            // Quaternion.LookRotation(player.position - myTransform.position), rotationSpeed * Time.deltaTime);
            //move
            if (distance > attackRange) {
                if (flying)
                    myTransform.position += (new Vector3(1, 0, 0) * speed * Time.deltaTime) * playerDirection;
                else if (didHit) {
                    Vector3 newPos = Vector3.MoveTowards(myTransform.position, player.position, speed * Time.deltaTime);
                    newPos.y = myTransform.position.y;
                    myTransform.position = newPos;
                }
            } else if (flying && Time.time > nextFire) {
                nextFire = Time.time + fireRate;
                // attack.logsomething();
                attack.Fire(player.position - myTransform.position, myTransform.position);
            }
        } else if (!flying) {
            myTransform.position += (new Vector3(1, 0, 0) * speed * Time.deltaTime) * direction;
        }
    }
}
