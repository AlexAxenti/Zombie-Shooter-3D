using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    private float weakZombieCooldown;
    private float fastZombieCooldown;

    public int weakZombieSpawnCount;
    public int fastZombieSpawnCount;
    public bool waveEnabled = true;

    public GameObject zombie;
    public GameObject fastZombie;
    //public GameObject player;

    WaveController waveController;

    // Use this for initialization
    void Start()
    {
        waveController = GetComponent<WaveController>();

        weakZombieSpawnCount = 0;
        fastZombieSpawnCount = 0;
        weakZombieCooldown = Time.time + 1f;
        fastZombieCooldown = Time.time + 2f;
    }

    // Update is called once per frame
    void Update()
    {

        if (weakZombieSpawnCount + fastZombieSpawnCount < waveController.weakZombieLimit + waveController.fastZombieLimit)
        {
            SpawnZombie();
        }

    }

    private Vector3 SpawnLocationRandomizer()
    {

        float spawnLocation = Random.Range(1, 8);

        string spawnObject = "ZombieSpawn" + spawnLocation;
        Vector3 spawnPos = new Vector3(0f, 0f, 0f);
        spawnPos = GameObject.Find(spawnObject).transform.position;
        spawnPos.y += 1f;
        
        return spawnPos;
    }

    private void SpawnZombie()
    {
        if (weakZombieCooldown < Time.time && waveEnabled == true && waveController.isPaused == false && (weakZombieSpawnCount < waveController.weakZombieLimit))
        {
            GameObject clone = zombie;
            Instantiate(clone, SpawnLocationRandomizer(), Quaternion.identity);
            weakZombieCooldown = Time.time + (1f - (waveController.wave * 0.025f));
            weakZombieSpawnCount++;
        }
        if(fastZombieCooldown < Time.time && waveEnabled == true && waveController.isPaused == false && (fastZombieSpawnCount < waveController.fastZombieLimit))
        {
            GameObject clone = fastZombie;
            Instantiate(clone, SpawnLocationRandomizer(), Quaternion.identity);
            //fastZombieCooldown = Time.time + (1f - (waveController.wave * 0.025f));
            fastZombieCooldown = Time.time + (2.5f - (waveController.wave * 0.025f));
            fastZombieSpawnCount++;
        }
    }
}
