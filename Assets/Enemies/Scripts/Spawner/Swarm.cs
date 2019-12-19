using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class Swarm : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject prefab;
        public int power;
    }
    public int swarmCount = 20;
    public int basePower = 5;
    public int AddPower = 5;
    public Text tt;
    private int wave;
    private int wavePower;
    public EnemyType[] enemyType;
    private List<GameObject> currentEnemies = new List<GameObject>();
    private Transform[] spawnPos;
    public Transform player;
    private int enemiesMinPower = 100;
    void Awake()
    {
        wavePower = basePower;
        spawnPos = GetComponentsInChildren<Transform>();
        foreach (EnemyType enemy in enemyType)
        {
            if (enemiesMinPower > enemy.power)
                enemiesMinPower = enemy.power;
        }
        CreateWave();
        wave = 1;
    }

    void FixedUpdate()
    {
        List<GameObject> tmpList = new List<GameObject>(currentEnemies);
        foreach(GameObject enemy in tmpList)
        {
            if (!enemy)
            {
                currentEnemies.Remove(enemy);
            }
        }
        if (currentEnemies.Count == 0) {
            tt.text = "WAVE N°" + wave;
            wave++;
            CreateWave();
        }
            
    }

    public void CreateWave() {
        int tmpPower = wavePower;
        int length = enemyType.Length;
        while (tmpPower >= enemiesMinPower)
        {
            int rand = Random.Range(0, length);
            if (enemyType[rand].power <= tmpPower)
            {
                int posRand = Random.Range(0, spawnPos.Length);
                GameObject enemy = Instantiate(enemyType[rand].prefab, spawnPos[posRand].position, Quaternion.identity);
                currentEnemies.Add(enemy);
                tmpPower -= enemyType[rand].power;
            }
        }
        wavePower += AddPower;
    }
}
