using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneload : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene() {
        staticField.scenenbr = index;
    }
}
