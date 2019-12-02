using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    public float speed = 1;
    public float timer = 2;
    private Rigidbody rigidBody;
    private GameObject canvas;
    public GameObject DisplayDamage;
    public GameObject player;
    private PlayerManager Mplayer;
    private WeaponManager Mweapon;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, timer);
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
        PlayerManager Mplayer = player.GetComponent<PlayerManager>();
        WeaponManager Mweapon = player.GetComponent<WeaponManager>();

        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth)
                enemyHealth.TakeDamage(Mweapon.GetComponent<WeaponManager>().damage);

            creatFloatingText(Mweapon.GetComponent<WeaponManager>().damage.ToString(), transform);
            Mplayer.GetComponent<PlayerManager>().point += Mweapon.GetComponent<WeaponManager>().damage;
            print(Mplayer.GetComponent<PlayerManager>().point);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Store"))
            buyFromStore(other, Mplayer, Mweapon);
    }

    private void buyFromStore(Collider other, PlayerManager Mplayer, WeaponManager Mweapon)
    {
        StoreType store = other.gameObject.GetComponent<StoreType>();

        //print(Mweapon.GetComponent<WeaponManager>().damage);
        if (store.storeType == StoreType.type.Damage && Mplayer.GetComponent<PlayerManager>().point >= store.cost)
        {
            print("initial damage :");
            print(Mweapon.GetComponent<WeaponManager>().damage);
            Mplayer.GetComponent<PlayerManager>().point -= store.cost;
            Mweapon.GetComponent<WeaponManager>().damage += (Mweapon.GetComponent<WeaponManager>().damage * (float)store.upgradePercentage / 100);
            store.cost += store.cost;
            print("upgarded damage :");
            print(Mweapon.GetComponent<WeaponManager>().damage);
        }
        if (store.storeType == StoreType.type.Life && Mplayer.GetComponent<PlayerManager>().point >= store.cost)
        {
            Mplayer.GetComponent<PlayerManager>().point -= store.cost;
            Mplayer.GetComponent<PlayerManager>().maxLife += (int)(Mplayer.GetComponent<PlayerManager>().maxLife * (float)store.upgradePercentage / 100);
            store.cost += store.cost;
            print("upgarded life :");
            print(Mplayer.GetComponent<PlayerManager>().maxLife);
        }

            Destroy(gameObject);
    }

    private void creatFloatingText(string Text, Transform pos)  
    {
        GameObject instance = Instantiate(DisplayDamage);

        instance.transform.SetParent(canvas.transform, false);
        instance.GetComponent<DamageDisplay>().setText(Text);
    }
}
