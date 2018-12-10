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
    public AudioClip welcome;
    public AudioClip knocked;
    public AudioClip reset;
    public AudioClip gameover;

    private Queue<AudioClip> toPlay = new Queue<AudioClip>();
    private float stall;

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

    public Text[] text_fields;
    private float text_timer;

    // Use this for initialization
    void Start () {
        stall = 0f;
        text_timer = 0f;
        SetText("init");
        current_leader = "None";
        AudioSource.PlayClipAtPoint(welcome, Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update () {
        stall -= Time.deltaTime;
        if (stall < 0 && toPlay.Count > 0) {
            AudioClip clip = toPlay.Dequeue();
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            stall = clip.length;
        }

        text_timer -= Time.deltaTime;
        if (text_timer < 0 && text_fields[0].text != "") {
            SetText("");
            //StartCoroutine(FadeOut());
        }

        scoretowin = GameManager.instance.TimeToWin;
        scores[0] = p1.GetCurrentScore();
        scores[1] = p2.GetCurrentScore();
        scores[2] = p3.GetCurrentScore();
        scores[3] = p4.GetCurrentScore();

        if (scores[0] > scores[1] && scores[0] > scores[2] && scores[0] > scores[3] && current_leader != "Player 1") {
            current_leader = "Player 1";
            SetText("Blue took the lead.");
            toPlay.Enqueue(leads[0]);
        }
        else if (scores[1] > scores[0] && scores[1] > scores[2] && scores[1] > scores[3] && current_leader != "Player 2")
        {
            current_leader = "Player 2";
            SetText("Red took the lead.");
            toPlay.Enqueue(leads[1]);
        }
        else if (scores[2] > scores[0] && scores[2] > scores[1] && scores[2] > scores[3] && current_leader != "Player 3")
        {
            current_leader = "Player 3";
            SetText("Green took the lead.");
            toPlay.Enqueue(leads[2]);
        }
        else if (scores[3] > scores[0] && scores[3] > scores[1] && scores[3] > scores[2] && current_leader != "Player 4")
        {
            current_leader = "Player 4";
            SetText("Yellow took the lead.");
            toPlay.Enqueue(leads[3]);
        }

        //Minute to wins
        for (int i = 0; i < 4; i++) {
            if (scoretowin - scores[i] == 60 && min_yes[i])
            {
                min_yes[i] = false;
                toPlay.Enqueue(minute[i]);
                if (i == 0) {  SetText("Blue needs a minute to win!"); }
                else if (i == 1) {  SetText("Red needs a minute to win!"); }
                else if (i == 2) {  SetText("Green needs a minute to win!"); }
                else if (i == 3) {  SetText("Yellow needs a minute to win!"); }
            }
        }

        //15 Seconds to wins
        for (int i = 0; i < 4; i++)
        {
            if (scoretowin - scores[i] == 15 && fif_yes[i])
            {
                fif_yes[i] = false;
                toPlay.Enqueue(fifteen[i]);
                if (i == 0) { SetText("Blue needs 15 seconds to win!"); }
                else if (i == 1) { SetText("Red needs 15 seconds to win!"); }
                else if (i == 2) { SetText("Green needs 15 seconds to win!"); }
                else if (i == 3) { SetText("Yellow needs 15 seconds to win!"); }
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
        text_timer = 3;
        string temp = text_fields[0].text;
        text_fields[0].text = s;
        text_fields[1].text = s;
        text_fields[2].text = s;
        text_fields[3].text = s;
        //if (temp == "") {
        //StartCoroutine(FadeIn());
        //}
    }
    /*
private IEnumerator FadeIn() {
    for (float i = 0f; i < .5f; i -= Time.deltaTime)
    {
        //text_fields[0].color = new Color(text_fields[0].color.r, text_fields[0].color.g, text_fields[0].color.b, i * 2);
        //text_fields[1].color = new Color(text_fields[1].color.r, text_fields[1].color.g, text_fields[1].color.b, i * 2);
        //text_fields[2].color = new Color(text_fields[2].color.r, text_fields[2].color.g, text_fields[2].color.b, i * 2);
        //text_fields[3].color = new Color(text_fields[3].color.r, text_fields[3].color.g, text_fields[3].color.b, i * 2);
        yield return null;
    }
}


private IEnumerator FadeOut()
{
    for (float i = .5f; i > 0f; i -= Time.deltaTime)
    {
        text_fields[0].color = new Color(text_fields[0].color.r, text_fields[0].color.g, text_fields[0].color.b, i * 2);
        text_fields[1].color = new Color(text_fields[1].color.r, text_fields[1].color.g, text_fields[1].color.b, i * 2);
        text_fields[2].color = new Color(text_fields[2].color.r, text_fields[2].color.g, text_fields[2].color.b, i * 2);
        text_fields[3].color = new Color(text_fields[3].color.r, text_fields[3].color.g, text_fields[3].color.b, i * 2);
        yield return null;
    }
    text_fields[0].text = "";
    text_fields[1].text = "";
    text_fields[2].text = "";
    text_fields[3].text = "";
}
*/
    public void TriggerReset()
    {
        toPlay.Enqueue(reset);
    }

    public void TriggerDrop(string who)
    {
        if (who == "player 1")
        {
            SetText("Blue dropped the flag!");
            toPlay.Enqueue(drops[0]);
        }
        else if (who == "player 2")
        {
            SetText("Red dropped the flag!");
            toPlay.Enqueue(drops[1]);
        }
        else if (who == "player 2")
        {
            SetText("Green dropped the flag!");
            toPlay.Enqueue(drops[2]);
        }
        else if (who == "player 2")
        {
            SetText("Yellow dropped the flag!");
            toPlay.Enqueue(drops[3]);
        }
    }

    public void Trigger(string who)
    {
        if (who == "player 1") {
            SetText("Blue took the flag!");
            toPlay.Enqueue(flags[0]);
        }
        else if (who == "player 2") {
            SetText("Red took the flag!");
            toPlay.Enqueue(flags[1]);
        }
        else if (who == "player 2") {
            SetText("Green took the flag!");
            toPlay.Enqueue(flags[2]);
        }
        else if (who == "player 2") {
            SetText("Yellow took the flag!");
            toPlay.Enqueue(flags[3]);
        }
    }
}
