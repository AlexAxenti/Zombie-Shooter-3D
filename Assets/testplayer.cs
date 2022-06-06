using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testplayer : MonoBehaviour {

    float speed = 0.2f;

    Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
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
        Vector3 playerVector = new Vector3(transform.position.x, 24.2f, transform.position.z - 20f);
        cam.transform.position = playerVector;
    }
}
