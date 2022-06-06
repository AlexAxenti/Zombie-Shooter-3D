using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public int health;
    public int lastHealth;

    private float damageCooldown;

    GameObject H1, H2, H3, H4;
    MeshRenderer mrender;
    WaveController waveController;

    // Use this for initialization
    void Start()
    {
        waveController = GameObject.Find("GameStateTracker").GetComponent<WaveController>();

        damageCooldown = Time.time + 0.75f;
        health = 4;
        H1 = GameObject.Find("Health1");
        H2 = GameObject.Find("Health2");
        H3 = GameObject.Find("Health3");
        H4 = GameObject.Find("Health4");
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth();
    }

    private void DisplayHealth()
    {
        switch (health)
        {
            case 0:
                ChangeSpriteRenderers(false, false, false, false);
                waveController.gameControl = WaveController.gameState.Ending;
                break;
            case 1:
                ChangeSpriteRenderers(true, false, false, false);
                break;
            case 2:
                ChangeSpriteRenderers(true, true, false, false);
                break;
            case 3:
                ChangeSpriteRenderers(true, true, true, false);
                break;
            default:
                ChangeSpriteRenderers(true, true, true, true);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" && Time.time > damageCooldown && health > 0)
        {
            health -= 1;
            lastHealth = health;
            damageCooldown = Time.time + 0.75f;
        }
    }

    private void ChangeSpriteRenderers(bool he1, bool he2, bool he3, bool he4)
    {
        mrender = H1.GetComponent<MeshRenderer>();
        if (he1)
        {
            mrender.enabled = true;
        }
        else
        {
            mrender.enabled = false;
        }

        mrender = H2.GetComponent<MeshRenderer>();
        if (he2)
        {
            mrender.enabled = true;
        }
        else
        {
            mrender.enabled = false;

        }

        mrender = H3.GetComponent<MeshRenderer>();
        if (he3)
        {
            mrender.enabled = true;
        }
        else
        {
            mrender.enabled = false;

        }

        mrender = H4.GetComponent<MeshRenderer>();
        if (he4)
        {
            mrender.enabled = true;
        }
        else
        {
            mrender.enabled = false;

        }

    }
}
