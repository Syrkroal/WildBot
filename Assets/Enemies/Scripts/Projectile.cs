using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1;
    public float timer = 2;
    public int damage = 1;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, timer);
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

        if (other.gameObject.CompareTag("Player"))
        {
            print("ok");
            PlayerManager player = other.gameObject.GetComponent<PlayerManager>();
            player.hitPlayer(damage);
        }
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground"))
            Destroy(gameObject);
    }
}
