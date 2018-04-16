using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundScript : MonoBehaviour {
    public GameObject[] gameButtons;
    bool roundChanging;
    bool gameEnding;

    int currentRound;

    AudioSource roundAudio;
    public AudioClip round1;
    public AudioClip round2;
    public AudioClip round3;
    AudioClip[] rounds;
   

    public PlayerScript[] players;

    // Use this for initialization
    void Start () {
        rounds = new AudioClip[3];
        rounds[0] = round1;
        rounds[1] = round2;
        rounds[2] = round3;
        roundAudio = GetComponent<AudioSource>();
        gameEnding = false;
        roundChanging = false;
        foreach(GameObject n in gameButtons)
        {
            n.SetActive(false);
        }
        StartCoroutine(StartGame());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (players[0].health < 0 || players[1].health < 0)
        {
            StartNewRound();
        }	
	}
    public void StartNewRound()
    {
        if (roundChanging == false)
        {
            Debug.Log("round Changing");
            roundChanging = true;
            currentRound++;
            foreach (GameObject n in gameButtons)
            {
                n.GetComponent<Button>().enabled = false;
            }
            if (players[0].health > players[1].health)
            {
                players[0].wins++;
            }
            else
            {
                players[1].wins++;
            }
            
            if (currentRound < rounds.Length)
            {
                foreach(PlayerScript n in players)
                {
                    n.health = n.startHealth;
                }
                StartCoroutine(StartRound());

            }
            else
            {  
                if (players[0].wins > players[1].wins)
                {
                    players[0].currentClip = players[0].youLose;
                    players[1].currentClip = players[1].youWin;
                }
                if (players[1].wins > players[0].wins)
                {
                    players[1].currentClip = players[1].youLose;
                    players[0].currentClip = players[0].youWin;
                }
                if (players[0].wins == players[1].wins)
                {
                }
                
                foreach (PlayerScript n in players)
                {
                    if (n.isPlaying == false)
                    {
                        n.isPlaying = true;
                        n.playerAudio.PlayOneShot(n.currentClip);
                        StartCoroutine(EndGame(n.currentClip.length));
                    }
                }
            }
            
            roundChanging = false;
        }
    }

    public IEnumerator StartRound()
    {
        roundAudio.PlayOneShot(rounds[currentRound]);
        yield return new WaitForSeconds(rounds[currentRound].length);
        foreach (GameObject n in gameButtons)
        {
            n.GetComponent<Button>().enabled = true;
        }
    }
    public IEnumerator StartGame()
    {
        roundAudio.PlayOneShot(rounds[0]);
        yield return new WaitForSeconds(rounds[0].length);
        foreach (GameObject n in gameButtons)
        {
            n.SetActive(true);
        }
    }
    public IEnumerator EndGame(float dur)
    {
        if (gameEnding == false)
        {
            gameEnding = true;
            yield return new WaitForSeconds(dur);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Honours");
        }
        else
        {
            yield break;
        }
    }
}
