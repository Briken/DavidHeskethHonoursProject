using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Authored by David Hesketh, Honours Project for Games Development(Software Development) ID: S1437170
*/
public class StartGame : MonoBehaviour {

    public GameObject game;
    public AudioSource startSound;
	// Use this for initialization
	void Start ()
    {
        startSound = GetComponent<AudioSource>();
        StartCoroutine(BeginGame());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator BeginGame()
    {
        startSound.PlayOneShot(startSound.clip);
        yield return new WaitForSeconds(startSound.clip.length);
        game.SetActive(true);
    }
}
