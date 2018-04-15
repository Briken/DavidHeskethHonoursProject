using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Authored by David Hesketh, Honours Project for Games Development(Software Development) ID: S1437170
*/
public class HeartRate : MonoBehaviour {
    public PlayerScript player;
    AudioSource heartbeat;
    float lastCheck;
	// Use this for initialization
	void Start ()
    {
        heartbeat = GetComponent<AudioSource>();
        lastCheck = player.startHealth;
	}
	// Update is called once per frame
	void Update ()
    {
        if (player.health == player.startHealth)
        {
            heartbeat.pitch = 1;
        }
		if (player.health != lastCheck)
        {
            lastCheck = player.health;
            Debug.Log(heartbeat.pitch);
            float pitchChange = (player.health / player.startHealth)/7;
            heartbeat.pitch += pitchChange;
            Debug.Log("pitch increased by: " + pitchChange + " new pitch: " + heartbeat.pitch);
        }
	}
}
