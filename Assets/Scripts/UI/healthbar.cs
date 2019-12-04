using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public GameObject player;
    public Image healthBar;

    private PlayerManager Mplayer;
    // Start is called before the first frame update
    void Start()
    {
         Mplayer = player.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
       healthBar.fillAmount = (float)Mplayer.currentLife / (float)Mplayer.maxLife;
        
    }
}
