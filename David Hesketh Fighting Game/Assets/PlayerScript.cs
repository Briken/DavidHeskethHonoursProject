using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
Authored by David Hesketh, Honours Project for Games Development(Software Development) ID: S1437170
*/
public class PlayerScript : MonoBehaviour {

    public GameObject RoundChange;
    public AudioClip punchSound;
    public AudioClip blockSound;
    public AudioClip damageSound;
    public AudioClip youWin;
    public AudioClip youLose;
    public bool isPlaying = false;
    public int wins;

    public AudioClip round1;
    public AudioClip round2;
    public AudioClip round3;
    AudioClip[] Rounds;

    public AudioClip deathSound;

    public Button[] myButtons = new Button[2];

    public int damage;
    public float startHealth;
    public float health;
    public int blockAmt;

    public AudioClip currentClip;
    
    public AudioSource playerAudio;

    public bool isPunching;
    public bool isBlocking;
    public bool roundChange;

    public int playerID;
    
    public PlayerScript opponent;
    
    // Use this for initialization
    void Start () {

        roundChange = false;
        
        playerAudio = GetComponent<AudioSource>();
        playerAudio.clip = currentClip;
        playerAudio.panStereo = playerID;
        playerAudio.PlayOneShot(currentClip);
        health = startHealth;
        #if UNITY_ANDROID || UNITY_EDITOR
        PhoneMode();
        #endif

    }

    // Update is called once per frame
    void Update ()
    {
        if (health < 0 && roundChange == false)
        {
            roundChange = true;
            currentClip = deathSound;
            StartCoroutine(PlayerDeath());
        }
    }
    private void PhoneMode()
    {
        foreach (Button n in myButtons)
        {
            n.enabled = true;
        }
    }
    public void StartPunch()
    {
        StartCoroutine(Punch());
    }

    
    public IEnumerator Punch()
    {
        playerAudio.panStereo = playerID;
        isPunching = true;
        currentClip = punchSound;
        foreach (Button n in myButtons)
        {
            n.GetComponent<Button>().enabled = false;
        }
        Debug.Log("play Audio");
        playerAudio.PlayOneShot(currentClip);
        yield return new WaitForSeconds(punchSound.length);
        Debug.Log("AudioPlayed");
        opponent.BeingPunched(damage);
        isPunching = false;
        foreach (Button n in myButtons)
        {
            n.GetComponent<Button>().enabled = true;
        }
    }

    public void Blocking()
    {
        Debug.Log("Blocking");
        isBlocking = true;
    }

    public void EndBlock()
    {
        isBlocking = false;
        Debug.Log("BlockEnded");
    }

    public void BeingPunched(int dmg)
    {
        float healthChange;
        if (isBlocking)
        {
            currentClip = blockSound;
            healthChange = (dmg - blockAmt);
        }
        else
        {
            currentClip = damageSound;
            healthChange = damage;
        }
        isBlocking = false;
        health -= healthChange;
        playerAudio.PlayOneShot(currentClip);
    }

    public IEnumerator PlayerDeath()
    {
        playerAudio.PlayOneShot(currentClip);
        yield return new WaitForSeconds(currentClip.length);


    }
    public IEnumerator PlayerEnd()
    {
        playerAudio.PlayOneShot(currentClip);
        yield return new WaitForSeconds(currentClip.length);
        roundChange = false;
    }
    
}
