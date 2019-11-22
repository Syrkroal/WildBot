using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float speed = 40;
    private Rigidbody rigidBody;
    private float x_min;
    private float x_max;
    private float z_min;
    private float z_max;

    private bool onGround;

    public float mouseSpeed = 4.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        KeyboardMovement();
        JumpEvent();
        MoveCamera();
    }

    private void KeyboardMovement()
    {
        float xMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float zMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float xSpeed = xMove * speed;
        float zSpeed = zMove * speed;
        Vector3 newVelocity = new Vector3(xSpeed, rigidBody.velocity.y, zSpeed);
        rigidBody.velocity = newVelocity;
    }

    private void JumpEvent()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            onGround = false;
            Vector3 newVelocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y + 3, rigidBody.velocity.z);
            rigidBody.velocity = newVelocity;
        }

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

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}