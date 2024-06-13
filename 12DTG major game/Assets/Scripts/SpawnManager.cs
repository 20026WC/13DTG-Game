using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject RangedEnemyPrefab;
    public GameObject CloseUpRangeEnemyPrefab;
    public GameObject powerupprefab;
    public int waveNumber = 1;
    public int enemyCount;
    private float spawnRange = 9.0f;
    private PlayerController PlayerController;
    // Start is called before the first frame update

    void Start()
    {
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {

        if (PlayerController.EndlessGameIsActive == true)
        {
            enemyCount = FindObjectsOfType<Enemie>().Length;
            if (enemyCount == 0) { waveNumber++; SpawnEnemyWave(waveNumber); Instantiate(powerupprefab, GenerateSpawnPosition(), powerupprefab.transform.rotation); }
        }
         
    }


    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 1.15f, spawnPosZ);

        return randomPos;
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i< enemiesToSpawn; i++)
        {
            Instantiate(RangedEnemyPrefab, new Vector3(0, 0, 6), RangedEnemyPrefab.transform.rotation);
            Instantiate(CloseUpRangeEnemyPrefab, new Vector3(0, 0, 6), CloseUpRangeEnemyPrefab.transform.rotation);
        }
    }

    public void StartOfGame()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupprefab, GenerateSpawnPosition(), powerupprefab.transform.rotation);
    }
}
