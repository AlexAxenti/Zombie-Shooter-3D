using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void resumeGame()
    {
        GetComponent<WaveController>().isPaused = false;
    }

    public void closeGame()
    {
        Application.Quit();
    }
}
