using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMotor : MonoBehaviour {

    NavMeshAgent agent;
    Rigidbody rigidbody;
    WaveController waveController;

	// Use this for initialization
	void Start () {
        waveController = GameObject.Find("GameStateTracker").GetComponent<WaveController>();
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (waveController.isPaused == true)
        {
            agent.destination = transform.position;
        }
        else
        {
            agent.destination = GameObject.Find("Player").transform.position;
            rigidbody.isKinematic = true;
            rigidbody.isKinematic = false;
        }
       
	}
}
