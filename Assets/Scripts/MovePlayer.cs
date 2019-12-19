using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private float x = 0;
    public float speed = 0;
    public float jumpForce = 0;
    public GameObject lightPrefabs;
    public GameObject lightPrefabs2;
    private bool jump = false;
    private bool inAir = false;
    private Rigidbody rb;
    public int playerNb = 1;
    public List<RuntimeAnimatorController> animList;
    private Animator anim;
    public GameObject GroundCheck;
    private GroundRaycast gc;
    public List<GameObject> followPlayer;
    public Vector3 LastCheckpont;
    public bool death = false;
    public int nblight = 4;
    public List<GameObject> power;
    public GameObject deathText;
    private float lightTimer = 0;
    private float deathTimer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gc = GroundCheck.GetComponent<GroundRaycast>();
        LastCheckpont = transform.position;
    }

    private void Update()
    {
        int tmp = 0;
        while (tmp != power.Count)
        {
            if (tmp > nblight - 1)
                power[tmp].SetActive(false);
            else
                power[tmp].SetActive(true);
            tmp++;
        }
        inAir = !gc.HasHit();
        if (playerNb == 1)
            InputCatcher();
        else if (playerNb == 2)
            InputCatcher2();
        if (x != 0 && inAir == false)
            anim.runtimeAnimatorController = animList[2];
        else if (inAir == false)
            anim.runtimeAnimatorController = animList[0];
        else
            anim.runtimeAnimatorController = animList[1];
        foreach (GameObject obj in followPlayer)
        {
            if (death == false)
            {
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y, obj.transform.position.z);
                obj.transform.position = newPos;
            }
        }
        if (x == 1)
            transform.eulerAngles = new Vector3(0, 90, 0);
        else if (x == -1)
            transform.eulerAngles = new Vector3(0, -90, 0);
        if (death == true && Input.anyKey && deathTimer <= Time.time)
        {
            deathText.SetActive(false);
            transform.position = LastCheckpont;
            death = false;
        }
    }

    private void FixedUpdate()
    {
        MovementManager();
    }

    void MovementManager()
    {
        transform.Translate(-transform.right * x * Time.deltaTime * speed);
        if (jump == true)
        {
            rb.AddForce(new Vector3(0, jumpForce * Time.deltaTime * 10, 0));
            jump = false;
        }
    }

    void InputCatcher2()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            x = 1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            x = -1;
        else
            x = 0;
        if (Input.GetKeyDown(KeyCode.O))
            Instantiate(lightPrefabs, transform.position, new Quaternion(0, 0, 0, 0));
        if (Input.GetKeyDown(KeyCode.P))
            Instantiate(lightPrefabs2, new Vector3(transform.position.x, transform.position.y, -20), new Quaternion(0, 0, 0, 0)).GetComponent<MovingLightX>().dir(transform.eulerAngles.y);
        if (Input.GetKeyDown(KeyCode.UpArrow) && inAir == false)
            jump = true;
    }

    void InputCatcher()
    {
        if (Input.GetKey(KeyCode.D))
            x = 1;
        else if (Input.GetKey(KeyCode.Q))
            x = -1;
        else
            x = 0;
        if (Input.GetKeyDown(KeyCode.E) && nblight > 0 && lightTimer <= Time.time)
        {
            lightTimer = Time.time + 3f;
            Instantiate(lightPrefabs, transform.position, new Quaternion(0, 0, 0, 0));
            nblight--;
        }
        if (Input.GetKeyDown(KeyCode.A) && nblight > 0 && lightTimer <= Time.time)
        {
            lightTimer = Time.time + 3f;
            Instantiate(lightPrefabs2, transform.position, new Quaternion(0, 0, 0, 0));
            nblight--;
        }
        if (Input.GetKeyDown(KeyCode.Space) && inAir == false)
            jump = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "death" && death == false)
        {
            deathText.SetActive(true);
            death = true;
            deathTimer = Time.time + 1f;
        }
    }
}
