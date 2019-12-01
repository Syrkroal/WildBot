using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    public float speed = 1;
    public float timer = 2;
    private float damage;
    private Rigidbody rigidBody;
    private GameObject Owner;
    private GameObject canvas;
    public GameObject DisplayDamage;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, timer);
        Owner = GameObject.FindGameObjectWithTag("Player");
        damage = Owner.GetComponent<WeaponManager>().damage;
        canvas = GameObject.Find("Canvas");
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
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth)
                enemyHealth.TakeDamage(damage);
            creatFloatingText(damage.ToString(), transform);
            Destroy(gameObject);
        }
    }

    private void creatFloatingText(string Text, Transform pos)  
    {
        GameObject instance = Instantiate(DisplayDamage);

        instance.transform.SetParent(canvas.transform, false);
        instance.GetComponent<DamageDisplay>().setText(Text);
    }
}
