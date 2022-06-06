using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    
    float speed = 0.2f;
    float pauseCooldown = 1;
    float waveDuration;


    Camera cam;
    new Rigidbody rigidbody;
    WaveController waveController;

    // Use this for initialization
    void Start()
    {
        waveDuration = 0;

        rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main;
        waveController = GameObject.Find("GameStateTracker").GetComponent<WaveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((waveController.gameControl == WaveController.gameState.Playing || waveController.gameControl == WaveController.gameState.Initiating) && waveController.isPaused == false) MovePlayer();
        pauseGame();
        if (waveDuration < waveController.wave) ResetStats();
    }

    private void pauseGame()
    {
        if (Input.GetKey(KeyCode.Escape) && pauseCooldown < Time.time)
        {
            waveController.isPaused = !waveController.isPaused;
            pauseCooldown = Time.time + 0.5f;
        }   
    }

    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f, 0f, speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0f, 0f, -speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed, 0f, 0f);
        }
        Vector3 playerVector = new Vector3(transform.position.x, 24.2f, transform.position.z-20f);
        cam.transform.position = playerVector;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "HealthBoost(Clone)")
        {
            Destroy(collision.gameObject);
            ResetStats();
            if (GetComponent<HealthController>().health < 4)
            {
                GetComponent<HealthController>().health = GetComponent<HealthController>().lastHealth + 1;
            }
            GetComponent<HealthController>().lastHealth = GetComponent<HealthController>().health;
        }
        else if (collision.gameObject.name == "MovementSpeedBoost(Clone)")
        {
            Destroy(collision.gameObject);
            ResetStats();
            speed = 0.25f;
            waveDuration = waveController.wave + 1;
        }
        else if (collision.gameObject.name == "MaxAmmoBoost(Clone)")
        {
            Destroy(collision.gameObject);
            ResetStats();
            GetComponent<ShootBullet>().maxAmmo = 20f;
            waveDuration = waveController.wave + 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.isKinematic = false;
    }


    private void ResetStats()
    {
        speed = 0.2f;
        GetComponent<ShootBullet>().maxAmmo = 15f;
        if (GetComponent<ShootBullet>().ammoCount > 15f) GetComponent<ShootBullet>().ammoCount = 15f;
    }

}