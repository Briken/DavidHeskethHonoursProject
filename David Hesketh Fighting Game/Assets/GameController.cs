using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    List<string> moves = new List<string>();
    public float roundTimer;

    AudioSource gameAudio;
    


    // Use this for initialization
    void Start()
    {
        gameAudio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        roundTimer += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            moves.Add("Punch");
        }
        if (Input.GetButton("Fire2"))
        {
            moves.Add("Block");
        }
    }

    
}
