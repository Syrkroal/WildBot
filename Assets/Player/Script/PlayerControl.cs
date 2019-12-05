using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody Rigid;
    public GameObject View;

    public float speed = 0.2f;
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    void MoveCamera()
    {
        yaw += mouseSpeed * Input.GetAxis("Mouse X");
        pitch -= mouseSpeed * Input.GetAxis("Mouse Y");

        View.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
    }
}