using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadSave : MonoBehaviour {
    // Start is called before the first frame update

    private int saveLevel;

    public List<GameObject> continues;
    public List<GameObject> levels;
    void Start () {
        string line;
        if (!File.Exists(Application.dataPath + "/Resources/save.txt"))
        {
            FileStream fs = File.Create(Application.dataPath + "/Resources/save.txt");
            List<string> tmp1 = new List<string>(){"1"};
            fs.Close();
            System.IO.File.WriteAllLines(Application.dataPath + "/Resources/save.txt", tmp1);
        }
        System.IO.StreamReader file = new System.IO.StreamReader (Application.dataPath + "/Resources/save.txt"); //load text file with data
        while ((line = file.ReadLine ()) != null) { //while text exists.. repeat           
            saveLevel = System.Convert.ToInt32(line);
        }
        file.Close ();

        for (int i = 0; i < saveLevel; i++)
        {
            levels[i].SetActive(true);
        }
        continues[saveLevel - 1].SetActive(true);
        
    }

    // Update is called once per frame
    void Update () {

    }
}