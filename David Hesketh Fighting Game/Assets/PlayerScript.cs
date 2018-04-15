using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {


    public AudioClip punchSound;
    public AudioClip blockSound;
    public AudioClip damageSound;

    public AudioClip youWin;
    public AudioClip youLose;

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

    AudioClip currentClip;
    
    public AudioSource playerAudio;

    public bool isPunching;
    public bool isBlocking;
    public bool roundChange;

    public int playerID;
    public int currentRound;
    public PlayerScript opponent;
    
    // Use this for initialization
    void Start () {
        Rounds = new AudioClip[3];
        Rounds[0] = round1;
        Rounds[1] = round2;
        Rounds[2] = round3;

        roundChange = false;
        currentRound = 0;
        playerAudio = GetComponent<AudioSource>();
        playerAudio.clip = currentClip;
        currentClip = Rounds[currentRound];
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
    public void CallNewRound()
    {
        StartCoroutine(NewRound());
    }
    public IEnumerator NewRound()
    {
        foreach (Button n in myButtons)
        {
            n.GetComponent<Button>().enabled = false;
        }
        currentRound++;
        if (health >= 0)
        {
            wins++;
        }
        if (currentRound < Rounds.Length)
        {
            currentClip = Rounds[currentRound];
        }
        else
        {
            Debug.Log("player " + playerID + "has " + wins + " wins");
            roundChange = true;
            if (wins > opponent.wins)
            {
                currentClip = youLose;
                Debug.Log("player " + playerID + "Wins");
            }
            if (wins < opponent.wins && currentRound == Rounds.Length)
            {
                currentClip = youWin;
                Debug.Log("player " + playerID + "Loses");
            }
        }
        playerAudio.PlayOneShot(currentClip);
        health = startHealth;
        Debug.Log("player" + playerID + " is at full hp");
        yield return new WaitForSeconds(currentClip.length);
        foreach (Button n in myButtons)
        {
            n.GetComponent<Button>().enabled = true;
        }
        if (currentRound == Rounds.Length)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Honours");
        }
        roundChange = false;
    }

    public IEnumerator PlayerDeath()
    {
        playerAudio.PlayOneShot(currentClip);
        yield return new WaitForSeconds(currentClip.length);
        opponent.CallNewRound();
        StartCoroutine(NewRound());

    }
    public IEnumerator PlayerEnd()
    {
        playerAudio.PlayOneShot(currentClip);
        yield return new WaitForSeconds(currentClip.length);
        roundChange = false;
    }
    
}
