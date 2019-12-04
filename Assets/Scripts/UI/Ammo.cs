using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public Text textammo;
    public Text texttotal;
    
    public GameObject player;

    private WeaponManager Mweapon;
    private PlayerManager Mplayer;

    // Start is called before the first frame update
    void Start()
    {
        Mweapon = player.GetComponent<WeaponManager>();
        Mplayer = player.GetComponent<PlayerManager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        textammo.text = Mweapon.bulletInLoader.ToString();
        texttotal.text = Mplayer.ammo.ToString();
    }
}
