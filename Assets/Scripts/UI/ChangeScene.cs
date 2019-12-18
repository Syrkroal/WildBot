using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clicked() {
        launchscene(staticField.scenenbr);
    }
    public void launchscene(int scene) {
        Time.timeScale = 1f;
        switch (scene)
        {
            case 1:
                SceneManager.LoadScene("SampleScene");
                staticField.actualLvl = 1;
                break;
            case 2:
                SceneManager.LoadScene("MainMenu2");
                break;
            case 3:
                SceneManager.LoadScene("test2");
                staticField.actualLvl = 3;
                break;
            case 4:
                SceneManager.LoadScene("test3");
                staticField.actualLvl = 4;
                break;
            default:
                break;
        }
    }
}