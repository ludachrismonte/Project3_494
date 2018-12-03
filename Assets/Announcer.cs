using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcer : MonoBehaviour {

    //blue, red, green, yellow
    public AudioClip[] leads;
    public AudioClip[] flags;
    public AudioClip[] fifteen;
    public AudioClip[] minute;
    public AudioClip[] wins;
    public AudioClip welcome;
    public AudioClip gameover;

    public Score p1;
    public Score p2;
    public Score p3;
    public Score p4;

    private int[] scores = { 0, 0, 0, 0 };

    private int scoretowin;
    private string current_leader;

    private bool[] min_yes = { true, true, true, true };
    private bool[] fif_yes = { true, true, true, true };
    private bool[] win_yes = { true, true, true, true };

    // Use this for initialization
    void Start () {
        current_leader = "None";
        AudioSource.PlayClipAtPoint(welcome, Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update () {
        scoretowin = p1.GetWinScore();
        scores[0] = p1.GetCurrentScore();
        scores[1] = p2.GetCurrentScore();
        scores[2] = p3.GetCurrentScore();
        scores[3] = p4.GetCurrentScore();

        if (scores[0] > scores[1] && scores[0] > scores[2] && scores[0] > scores[3] && current_leader != "Player 1") {
            current_leader = "Player 1";
            StartCoroutine(Play(leads[0]));
        }
        else if (scores[1] > scores[0] && scores[1] > scores[2] && scores[1] > scores[3] && current_leader != "Player 2")
        {
            current_leader = "Player 2";
            StartCoroutine(Play(leads[1]));
        }
        else if (scores[2] > scores[0] && scores[2] > scores[1] && scores[2] > scores[3] && current_leader != "Player 3")
        {
            current_leader = "Player 3";
            StartCoroutine(Play(leads[2]));
        }
        else if (scores[3] > scores[0] && scores[3] > scores[1] && scores[3] > scores[2] && current_leader != "Player 4")
        {
            current_leader = "Player 4";
            StartCoroutine(Play(leads[3]));
        }

        //Minute to wins
        for (int i = 0; i < 4; i++) {
            if (scoretowin - scores[i] == 60 && min_yes[i])
            {
                min_yes[i] = false;
                StartCoroutine(Play(minute[i]));
            }
        }

        //15 Seconds to wins
        for (int i = 0; i < 4; i++)
        {
            if (scoretowin - scores[i] == 15 && fif_yes[i])
            {
                fif_yes[i] = false;
                StartCoroutine(Play(fifteen[i]));
            }
        }

        //Wins
        for (int i = 0; i < 4; i++)
        {
            if (scoretowin == scores[i] && win_yes[i])
            {
                win_yes[i] = false;
                AudioSource.PlayClipAtPoint(wins[i], Camera.main.transform.position);
            }
        }
    }

    private IEnumerator Play(AudioClip clip) {
        yield return new WaitForSeconds(2);
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    public void Trigger(string who)
    {
        if (who == "player 1") {
            AudioSource.PlayClipAtPoint(flags[0], Camera.main.transform.position);
        }
        else if (who == "player 2") {
            AudioSource.PlayClipAtPoint(flags[1], Camera.main.transform.position);
        }
        else if (who == "player 2") {
            AudioSource.PlayClipAtPoint(flags[2], Camera.main.transform.position);
        }
        else if (who == "player 2") {
            AudioSource.PlayClipAtPoint(flags[3], Camera.main.transform.position);
        }
    }
}
