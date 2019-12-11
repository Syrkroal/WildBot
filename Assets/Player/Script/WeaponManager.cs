using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject projectile;
    public GameObject weapon;
    public GameObject childWeapon;
    public GameObject parentWeapon;
    public Transform recoilStart;
    public Transform recoilBack;
    public Transform NormalShoot;
    public Transform PreciseShoot;
    public Camera wCamera;

    public float fireRate = 0.2f;
    private float nextFire = 0;
    public float recoilSpeed = 1f;
    public float recoilLaps = 0.05f;
    public int loaderSize = 20;
    public float damage = 10.0f;
    public float headshotMultiplier = 2.5f;
    public int bulletInLoader;

    public float lockViewSpeed = 1.5f;
    public float PreciseView = 10;

    private Animator anim;
    private AnimationClip [] clips;
    private float loadingTime;
    private bool isReloading = false;
    private bool isAiming = false;
    private float loadingtimeLeft;
    private float currentView = 0;
    void Awake()
    {
        bulletInLoader = loaderSize;
        damage = 10.0f;
        print(damage);
        anim = childWeapon.GetComponent<Animator>();
        clips = anim.runtimeAnimatorController.animationClips;
        loadingTime = clips[0].length;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !isReloading)
        {
            Fire(parentWeapon.transform.forward, weapon.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            loadingtimeLeft = loadingTime + Time.time;
            transform.GetComponent<PlayerManager>().reload(loaderSize, bulletInLoader);
            bulletInLoader = loaderSize;
            childWeapon.GetComponent<Animator>().Play("reloadAnim", 0);
        }
        replaceWeapon();

        if (loadingtimeLeft < Time.time)
            isReloading = false;
        if (Time.time < (nextFire + recoilLaps - fireRate))
            doRecoil();
        if (Input.GetMouseButton(1) && !isReloading)
        {
            if (!isAiming)
            {
                GetComponent<PlayerControl>().speed = GetComponent<PlayerControl>().speed / 2;
                GetComponent<PlayerControl>().mouseSpeed = GetComponent<PlayerControl>().mouseSpeed / 2;
                isAiming = true;
            }
            GotoPreciseView();
        }
        else
        {
            if (isAiming)
            {
                GetComponent<PlayerControl>().speed = GetComponent<PlayerControl>().speed * 2;
                GetComponent<PlayerControl>().mouseSpeed = GetComponent<PlayerControl>().mouseSpeed * 2;
                isAiming = false;
            }
            GotoNormalView();
        }
    }


    public void Fire(Vector3 direction, Vector3 spawnPos)
    {
        if (Time.time > nextFire)
        {
            bulletInLoader = transform.GetComponent<PlayerManager>().shootAmmo(bulletInLoader);
            if (bulletInLoader > 0)
            {
                nextFire = Time.time + fireRate;
                GameObject go = Instantiate(projectile, spawnPos, Quaternion.identity);
                go.GetComponent<ProjectilePlayer>().setDirection(direction);
            }
        }
    }

    public void doRecoil()
    {
        weapon.transform.position = Vector3.MoveTowards(weapon.transform.position, recoilBack.position, Time.deltaTime * recoilSpeed);
    }

    public void replaceWeapon()
    {
        weapon.transform.position = Vector3.MoveTowards(weapon.transform.position, recoilStart.position, Time.deltaTime * recoilSpeed / 2);
    }

    public void GotoPreciseView()
    {
        if (currentView < PreciseView)
        {
            currentView++;
            wCamera.fieldOfView--;
        }
        parentWeapon.transform.position = Vector3.MoveTowards(parentWeapon.transform.position, PreciseShoot.position, Time.deltaTime * lockViewSpeed);
    }

    public void GotoNormalView()
    {
        if (currentView > 0)
        {
            currentView--;
            wCamera.fieldOfView++;
        }
        parentWeapon.transform.position = Vector3.MoveTowards(parentWeapon.transform.position, NormalShoot.position, Time.deltaTime * lockViewSpeed);
    }
}