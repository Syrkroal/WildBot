using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float speed = 40;
    private Rigidbody rigidBody;
    private float x_min;
    private float x_max;
    private float z_min;
    private float z_max;

    void Awake (){
        rigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
        KeyboardMovement();
    }

    private void KeyboardMovement (){
        float xMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float zMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float xSpeed = xMove * speed;
        float zSpeed = zMove * speed;
        Vector3 newVelocity = new Vector3(xSpeed, 0, zSpeed);
        rigidBody.velocity = newVelocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth)
                enemyHealth.TakeDamage(1);
            print("Hit");
        }
    }
}