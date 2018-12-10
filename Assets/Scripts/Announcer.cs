using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Announcer : MonoBehaviour {

    //blue, red, green, yellow
    public AudioClip[] leads;
    public AudioClip[] flags;
    public AudioClip[] drops;
    public AudioClip[] fifteen;
    public AudioClip[] minute;
    public AudioClip[] wins;
    public AudioClip knocked;
    public AudioClip reset;
    public AudioClip gameover;

    private Queue<AudioClip> toPlay = new Queue<AudioClip>();
    private Queue<string> toDisplay = new Queue<string>();
    private float stall;
    public Text[] text_fields;

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
        stall = 0f;
        SetText("");
        current_leader = "None";
    }

    // Update is called once per frame
    void Update () {
        stall -= Time.deltaTime;
        if (stall < 0 && toPlay.Count > 0) {
            AudioClip clip = toPlay.Dequeue();
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            string temp = toDisplay.Dequeue();
            SetText(temp);
            stall = clip.length;
        }

        if (stall < 0) {
            SetText("");
        }

        scoretowin = GameManager.instance.TimeToWin;
        scores[0] = p1.GetCurrentScore();
        scores[1] = p2.GetCurrentScore();
        scores[2] = p3.GetCurrentScore();
        scores[3] = p4.GetCurrentScore();

        if (scores[0] > scores[1] && scores[0] > scores[2] && scores[0] > scores[3] && current_leader != "Player 1") {
            current_leader = "Player 1";
            toPlay.Enqueue(leads[0]);
            toDisplay.Enqueue("Blue took the lead.");
        }
        else if (scores[1] > scores[0] && scores[1] > scores[2] && scores[1] > scores[3] && current_leader != "Player 2")
        {
            current_leader = "Player 2";
            toDisplay.Enqueue("Red took the lead.");
            toPlay.Enqueue(leads[1]);
        }
        else if (scores[2] > scores[0] && scores[2] > scores[1] && scores[2] > scores[3] && current_leader != "Player 3")
        {
            current_leader = "Player 3";
            toDisplay.Enqueue("Green took the lead.");
            toPlay.Enqueue(leads[2]);
        }
        else if (scores[3] > scores[0] && scores[3] > scores[1] && scores[3] > scores[2] && current_leader != "Player 4")
        {
            current_leader = "Player 4";
            toDisplay.Enqueue("Yellow took the lead.");
            toPlay.Enqueue(leads[3]);
        }

        //Minute to wins
        for (int i = 0; i < 4; i++) {
            if (scoretowin - scores[i] == 60 && min_yes[i])
            {
                min_yes[i] = false;
                toPlay.Enqueue(minute[i]);
                if (i == 0) { toDisplay.Enqueue("Blue needs a minute to win!."); }
                else if (i == 1) { toDisplay.Enqueue("Red needs a minute to win!."); }
                else if (i == 2) { toDisplay.Enqueue("Green needs a minute to win!."); }
                else if (i == 3) { toDisplay.Enqueue("Yellow needs a minute to win!."); }
            }
        }

        //15 Seconds to wins
        for (int i = 0; i < 4; i++)
        {
            if (scoretowin - scores[i] == 15 && fif_yes[i])
            {
                fif_yes[i] = false;
                toPlay.Enqueue(fifteen[i]);
                if (i == 0) { toDisplay.Enqueue("Blue needs 15 seconds to win!."); }
                else if (i == 1) { toDisplay.Enqueue("Red needs 15 seconds to win!."); }
                else if (i == 2) { toDisplay.Enqueue("Green needs 15 seconds to win!."); }
                else if (i == 3) { toDisplay.Enqueue("Yellow needs 15 seconds to win!."); }
            }
        }

        //Wins
        for (int i = 0; i < 4; i++)
        {
            if (scoretowin == scores[i] && win_yes[i])
            {
                win_yes[i] = false;
                toPlay.Enqueue(wins[i]);
            }
        }
    }

    private void SetText(string s) {
        text_fields[0].text = s;
        text_fields[1].text = s;
        text_fields[2].text = s;
        text_fields[3].text = s;
    }

    public void TriggerReset()
    {
        toPlay.Enqueue(reset);
    }

    public void TriggerDrop(string who)
    {
        if (who == "player 1")
        {
            toDisplay.Enqueue("Blue dropped the flag!");
            toPlay.Enqueue(drops[0]);
        }
        else if (who == "player 2")
        {
            toDisplay.Enqueue("Red dropped the flag!");
            toPlay.Enqueue(drops[1]);
        }
        else if (who == "player 3")
        {
            toDisplay.Enqueue("Green dropped the flag!");
            toPlay.Enqueue(drops[2]);
        }
        else if (who == "player 4")
        {
            toDisplay.Enqueue("Yellow dropped the flag!");
            toPlay.Enqueue(drops[3]);
        }
    }

    public void Trigger(string who)
    {
        Debug.Log(who + " took it");
        if (who == "player 1") {
            toDisplay.Enqueue("Blue took the flag!");
            toPlay.Enqueue(flags[0]);
        }
        else if (who == "player 2") {
            toDisplay.Enqueue("Red took the flag!");
            toPlay.Enqueue(flags[1]);
        }
        else if (who == "player 3") {
            toDisplay.Enqueue("Green took the flag!");
            toPlay.Enqueue(flags[2]);
        }
        else if (who == "player 4") {
            toDisplay.Enqueue("Yellow took the flag!");
            toPlay.Enqueue(flags[3]);
        }
    }
}
