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
        if (other.gameObject.CompareTag("EnemyHead"))
        {
            print("headshot!");
            EnemyHealth enemyHealth = other.transform.root.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth)
                enemyHealth.TakeDamage(Mweapon.damage * Mweapon.headshotMultiplier);
            creatFloatingText((Mweapon.damage * Mweapon.headshotMultiplier).ToString(), transform);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.transform.root.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth)
                enemyHealth.TakeDamage(Mweapon.damage);
            else
            {
                enemyHealth = other.transform.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth)
                    enemyHealth.TakeDamage(Mweapon.damage);
            }

            int tmpInt = (int)Mweapon.damage;
            creatFloatingText(tmpInt.ToString(), transform);
            Mplayer.point += tmpInt;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Store"))
            buyFromStore(other, Mplayer, Mweapon);
    }

    private void buyFromStore(Collider other, PlayerManager Mplayer, WeaponManager Mweapon)
    {
        StoreType store = other.gameObject.GetComponent<StoreType>();

        if (store.storeType == StoreType.type.Damage && Mplayer.point >= store.cost)
        {
            Mplayer.point -= store.cost;
            Mweapon.damage += (Mweapon.damage * (float)store.upgradePercentage / 100);
            store.cost += store.cost;
            print("upgarded damage :");
            print(Mweapon.damage);
        }
        if (store.storeType == StoreType.type.Life && Mplayer.point >= store.cost)
        {
            Mplayer.point -= store.cost;
            Mplayer.maxLife += (int)(Mplayer.maxLife * (float)store.upgradePercentage / 100);
            store.cost += store.cost;
            print("upgarded life :");
            print(Mplayer.maxLife);
        }
        if (store.storeType == StoreType.type.LoaderSize && Mplayer.point >= store.cost)
        {
            Mplayer.point -= store.cost;
            Mweapon.loaderSize += (int)(Mweapon.loaderSize * (float)store.upgradePercentage / 100);
            store.cost += store.cost;
            print("upgarded loader :");
            print(Mweapon.loaderSize);
        }
        if (store.storeType == StoreType.type.FireRate && Mplayer.point >= store.cost)
        {
            print(Mweapon.fireRate);
            Mplayer.point -= store.cost;
            Mweapon.fireRate -= (Mweapon.fireRate * (float)store.upgradePercentage / 100);
            store.cost += store.cost;
            print("upgarded firerate :");
            print(Mweapon.fireRate);
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
