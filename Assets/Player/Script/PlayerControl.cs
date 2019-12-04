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
        float yMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float xSpeed = xMove * speed;
        float ySpeed = yMove * speed;
        Vector3 newVelocity = new Vector3(xSpeed, ySpeed, 0);
        // Vector3 newVelocity = new Vector3(xSpeed, 0, 0);
        rigidBody.velocity = newVelocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.CompareTag("Enemy")) {
        //     EnemyStat stat = collision.gameObject.GetComponent<EnemyStat>();
        //     if (stat)
        //         stat.TakeDamage(1);
        // }
    }
}