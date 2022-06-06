using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{
    public GameObject healthBoost;
    public GameObject movementSpeedBoost;
    public GameObject maxAmmoBoost;

    public enum gameState { Initiating, Playing, Ending, Paused }
    public gameState gameControl = gameState.Initiating;
    public int wave = 1;
    public bool isPaused = false;

    private float alpha = 0;
    private float boostCooldown;
    private float waveCooldown;
    private bool boostSpawned = false;

    public int weakZombieLimit;
    public int fastZombieLimit;
    public int zombieDestroyedCount;
    public Text gameText;
    public Image endImage;

    ZombieSpawner zombieSpawner;

    // Use this for initialization
    void Start()
    {
        zombieSpawner = GetComponent<ZombieSpawner>();

        zombieDestroyedCount = 0;
        fastZombieLimit = 0;
        weakZombieLimit = 0;
        wave = 0;
        waveCooldown = Time.time + 3;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("PauseGameCanvas").GetComponent<Canvas>().enabled = isPaused;
        if (isPaused == true)
        {

        }
        else if (gameControl == gameState.Playing)
        {
            ChangeWave();
        }
        else if (gameControl == gameState.Initiating)
        {
            startGame();
        }
        else if (gameControl == gameState.Ending)
        {
            endGame();
        }
    }

    private void endGame()
    {
        gameText.text = "Game Over!";
        alpha += 0.005f;
        endImage.color = new Vector4(255f, 0f, 0f, alpha);
        if (alpha >= 1) SceneManager.LoadScene(2);
    }

    private void startGame()
    {
        gameText.text = "Waves Beginning";
        zombieSpawner.waveEnabled = false;
        //if (Time.time > waveCooldown)
        //{
            gameControl = gameState.Playing;
        //}
    }

    private void ChangeWave()
    {
        if (zombieDestroyedCount >= weakZombieLimit + fastZombieLimit)
        {
            if(wave!=0) gameText.text = "Wave " + (wave) + " Passed";

            waveCooldown = Time.time + 3;

            boostCooldown = waveCooldown + 3;
            boostSpawned = false;

            zombieSpawner.weakZombieSpawnCount = 0;
            zombieSpawner.fastZombieSpawnCount = 0;
            zombieSpawner.waveEnabled = false;

            wave++;
            weakZombieLimit = wave * 3;
            fastZombieLimit = wave - 1;
            zombieDestroyedCount = 0;

            GameObject boost = GameObject.FindGameObjectWithTag("Boost");
            if (boost != null) Destroy(boost.gameObject);
        }
        if (Time.time > waveCooldown)
        {
            gameText.text = "";
            zombieSpawner.waveEnabled = true;
            SpawnBoost(Random.Range(1,4));
        }
    }

    private void SpawnBoost(int boostType)
    {
        if(Time.time > boostCooldown && boostSpawned == false)
        {
            boostSpawned = true;
            Vector3 spawnLocation = GameObject.Find("BoostSpawn" + Random.Range(1, 4)).transform.position;
            spawnLocation.y += 1;

            if (boostType == 1)
            {
                GameObject boostClone = Instantiate(healthBoost, spawnLocation, Quaternion.identity);
            }
            else if(boostType == 2)
            {
                GameObject boostClone = Instantiate(movementSpeedBoost, spawnLocation, Quaternion.identity);
            }
            else if(boostType == 3)
            {
                GameObject boostClone = Instantiate(maxAmmoBoost, spawnLocation, Quaternion.identity);

            }
        }
    }

    /*private string RandomBoostSpawn()
    {
        int spawnLocation = Random.Range(1, 4);
        string spawnLocationStr = "" + spawnLocation;
        return (spawnLocationStr);
    }*/
}
