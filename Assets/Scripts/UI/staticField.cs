using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticField : MonoBehaviour
{
    static public int scenenbr = 1;
    static public int actualLvl = 0;

    static public bool isLaunching = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launch(){
        isLaunching = true;
    }

}
