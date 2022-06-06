using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootBullet : MonoBehaviour
{

    public GameObject Bullet;
    public Text ammoText;

    float shootCooldown;
    public float maxAmmo = 15f;
    public float ammoCount = 15f;
    float ammoCooldown;

    Vector3 bulletPos;

    WaveController waveController;

    void Start()
    {
        ammoText.text = ammoCount + "/" + maxAmmo;
        ammoCooldown = Time.time - 1f;
        shootCooldown = Time.time + 0.2f;
        waveController = GameObject.Find("GameStateTracker").GetComponent<WaveController>();
    }

    void Update()
    {
        InitiateShootBullet();
        ReloadAmmo();
    }

    private void InitiateShootBullet()
    {
        if (Input.GetMouseButton(0) && shootCooldown <= Time.time && waveController.gameControl == WaveController.gameState.Playing && ammoCooldown < Time.time && waveController.isPaused == false)
        {
            bulletPos = transform.position;
            ammoCount -= 1;

            CreateBulletClone(bulletPos);

            shootCooldown = Time.time + 0.2f;  
        }
    }

    private void CreateBulletClone(Vector3 bulletPos)
    {
        GameObject clone = (GameObject)Instantiate(Bullet, bulletPos, Quaternion.identity);
        clone.GetComponent<BulletMotor>().SetTarget(Input.mousePosition);
    }

    private void ReloadAmmo()
    {
        if(ammoCount <= 0 || ammoCooldown > Time.time || Input.GetKeyDown(KeyCode.R))
        {
            if (ammoCount <= 0 || Input.GetKeyDown(KeyCode.R))
            {
                ammoCooldown = Time.time + 2f;
            }
            ammoCount = maxAmmo;
            ammoText.text = "Reloading...";
            
        }
        else if (ammoCooldown < Time.time)
        {
            ammoText.text = ammoCount + "/" + maxAmmo;
        }
    }
}
