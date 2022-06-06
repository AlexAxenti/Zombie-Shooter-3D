using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateMainMenu : MonoBehaviour {

    public GameObject cameraTarget;

    float timeCounter = 0;

    float speed;
    public float width;
    public float length;

	// Use this for initialization
	void Start () {
        speed = 0.5f;
        width = 60;
        length = 100;
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;
        float z = Mathf.Sin(timeCounter) * length;
        float y = 40;

        transform.LookAt(cameraTarget.transform);
        transform.position = new Vector3(x, y, z);

    }
}
