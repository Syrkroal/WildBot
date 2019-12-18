using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause  : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public static bool paused;
    void Start()
    {
        // Time.timeScale = 1f;
        //canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = togglePause();
        }
    }

    public void resume() {
        Debug.Log("toto");
        Time.timeScale = 1f;
        canvas.SetActive(false);
        player.GetComponent<PlayerControl>().isPaused = true;
        
    }

    public void exit() {
        Time.timeScale = 1f;
        canvas.SetActive(false);
        player.GetComponent<PlayerControl>().isPaused = false;
        SceneManager.LoadScene("MenuPause");
    }
    bool togglePause()
     {
         if(Time.timeScale == 0f)
         {
            Debug.Log("on");
             Time.timeScale = 1f;
             canvas.SetActive(false);
             player.GetComponent<PlayerControl>().isPaused = false;
             return(false);
         }
         else
         {
            Debug.Log("off");
            canvas.SetActive(true);
            Time.timeScale = 0f;
            player.GetComponent<PlayerControl>().isPaused = true;
            return(true);    
         }
     }
}
