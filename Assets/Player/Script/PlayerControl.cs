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

    private bool onGround;
    public float JumpForce;
    public float mouseSpeed = 4.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    void Awake()
    {
        Cursor.visible = false;
        Rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        KeyboardMovement();
        JumpEvent();
        MoveCamera();
    }

    private void KeyboardMovement()
    {
        float tmpSpeed = speed;
        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") > 0) tmpSpeed = speed / 2;
        Rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * speed) + (transform.right * Input.GetAxis("Horizontal") * speed));
        if (Input.GetKeyDown("space"))
            Rigid.AddForce(transform.up * JumpForce);
    }

    private void JumpEvent()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            onGround = false;
            Vector3 newVelocity = new Vector3(Rigid.velocity.x, Rigid.velocity.y + 5, Rigid.velocity.z);
            Rigid.velocity = newVelocity;
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

        print(pitch);
        if (pitch > 90) pitch = 90;
        if (pitch < -90) pitch = -90;

        View.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
    }
}