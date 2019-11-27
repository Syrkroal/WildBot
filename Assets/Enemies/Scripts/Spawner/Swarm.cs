using UnityEngine;
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
    private int wavePower;
    public EnemyType[] enemyType;
    private List<GameObject> currentEnemies = new List<GameObject>();
    public Transform player;
    void Awake()
    {
        wavePower = basePower;
        CreateWave();
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
        if (currentEnemies.Count == 0)
            CreateWave();
    }

    public void CreateWave() {
        int tmpPower = wavePower;
        int length = enemyType.Length;
        while (tmpPower != 0)
        {
            int rand = Random.Range(0, length);
            if (enemyType[rand].power <= tmpPower)
            {
                GameObject enemy = Instantiate(enemyType[rand].prefab, transform.position, Quaternion.identity);
                currentEnemies.Add(enemy);
                // Instantiate(enemies[length].prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                tmpPower -= enemyType[rand].power;
            }
        }
        wavePower += AddPower;
    }
}
