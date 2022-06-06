using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMotor : MonoBehaviour
{

    private float speed = 30f;
    private Vector3 target;
    private float bulletTravelTime;
    private Vector3 bulletMovement = new Vector3(0f, 0f, 0f);


    Camera cam;

    WaveController waveController;

    void Start()
    {
        bulletTravelTime = Time.time + 1.5f;
        waveController = GameObject.Find("GameStateTracker").GetComponent<WaveController>();
    }

    void Update()
    {
        moveBullet();
    }

    private void moveBullet()
    {
        transform.position += bulletMovement;
        if (bulletTravelTime < Time.time)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Vector3 mousePos)
    {
        cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            target = hit.point;

            float bangle = Mathf.Atan2(transform.position.z - target.z, transform.position.x - target.x);

            float xlength = Mathf.Cos(bangle)* speed * Time.deltaTime;
            float zlength = Mathf.Sin(bangle) * speed * Time.deltaTime;

            bulletMovement.x = -xlength;
            bulletMovement.y = 0f;
            bulletMovement.z = -zlength;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            waveController.zombieDestroyedCount += 1;
        }
        if(other.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
