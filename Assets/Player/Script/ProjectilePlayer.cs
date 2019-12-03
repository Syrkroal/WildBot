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

            int tmpInt = (int)Mweapon.GetComponent<WeaponManager>().damage;
            creatFloatingText(tmpInt.ToString(), transform);
            Mplayer.GetComponent<PlayerManager>().point += tmpInt;
            print(Mplayer.GetComponent<PlayerManager>().point);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Store"))
            buyFromStore(other, Mplayer, Mweapon);
    }

    private void buyFromStore(Collider other, PlayerManager Mplayer, WeaponManager Mweapon)
    {
        StoreType store = other.gameObject.GetComponent<StoreType>();

        if (store.storeType == StoreType.type.Damage && Mplayer.GetComponent<PlayerManager>().point >= store.cost)
        {
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
        if (store.storeType == StoreType.type.LoaderSize && Mplayer.GetComponent<PlayerManager>().point >= store.cost)
        {
            Mplayer.GetComponent<PlayerManager>().point -= store.cost;
            Mweapon.GetComponent<WeaponManager>().loaderSize += (int)(Mweapon.GetComponent<WeaponManager>().loaderSize * (float)store.upgradePercentage / 100);
            store.cost += store.cost;
            print("upgarded loader :");
            print(Mweapon.GetComponent<WeaponManager>().loaderSize);
        }
        if (store.storeType == StoreType.type.FireRate && Mplayer.GetComponent<PlayerManager>().point >= store.cost)
        {
            print(Mweapon.GetComponent<WeaponManager>().fireRate);
            Mplayer.GetComponent<PlayerManager>().point -= store.cost;
            Mweapon.GetComponent<WeaponManager>().fireRate -= (Mweapon.GetComponent<WeaponManager>().fireRate * (float)store.upgradePercentage / 100);
            store.cost += store.cost;
            print("upgarded firerate :");
            print(Mweapon.GetComponent<WeaponManager>().fireRate);
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
