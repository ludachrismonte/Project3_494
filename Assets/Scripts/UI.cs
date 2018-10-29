using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    public Text timer;
    public Text [] speedometer;
    public Text [] healthText;
    //public Button quit;
    public float allotedTime; //Length of round in seconds
    float startTime;
    Rigidbody [] carRb;
    Health [] health;
    string kmPerHour = "km/hr";
    //public bool changeScene = false;
	// Use this for initialization
	void Start () {
        carRb = new Rigidbody[4];
        health = new Health[4];

        startTime = Time.time;
        carRb[0] = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        carRb[1] = GameObject.FindWithTag("Player2").GetComponent<Rigidbody>();
        carRb[2] = GameObject.FindWithTag("Player3").GetComponent<Rigidbody>();
        carRb[3] = GameObject.FindWithTag("Player4").GetComponent<Rigidbody>();
        //health = GameObject.FindWithTag("Player").GetComponent<Health>();
        health[0] = GameObject.FindWithTag("Player").GetComponent<Health>();
        health[1] = GameObject.FindWithTag("Player2").GetComponent<Health>();
        health[2] = GameObject.FindWithTag("Player3").GetComponent<Health>();
        health[3] = GameObject.FindWithTag("Player4").GetComponent<Health>();
	}
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.Escape)){
            StartCoroutine(changeScence()); 
        }

        //Timer
        float timeTillEnd = allotedTime - (Time.time - startTime);
        if(timeTillEnd<0){
            timeTillEnd = 0;
            timer.color = Color.yellow;
            StartCoroutine(changeScence());
        }
        string minutes = ((int)timeTillEnd / 60).ToString();
        string seconds = (timeTillEnd % 60).ToString("f2");
        timer.text = minutes + ":" + seconds; 

        //SpeedoMeter
        //Player1
        string speed = ((int)carRb[0].velocity.magnitude * 3.6f).ToString("f2");
        speedometer[0].text = speed + " " + kmPerHour;
        //Player2
        speed = ((int)carRb[1].velocity.magnitude * 3.6f).ToString("f2");
        speedometer[1].text = speed + " " + kmPerHour;
        //Player3
        speed = ((int)carRb[2].velocity.magnitude * 3.6f).ToString("f2");
        speedometer[2].text = speed + " " + kmPerHour;
        //Player4
        speed = ((int)carRb[3].velocity.magnitude * 3.6f).ToString("f2");
        speedometer[3].text = speed + " " + kmPerHour;

        //Health
        //Player1
        healthText[0].text = health[0].health.ToString("f2");
        //Player2
        healthText[1].text = health[1].health.ToString("f2");
        //Player3
        healthText[2].text = health[2].health.ToString("f2");
        //Player4
        healthText[3].text = health[3].health.ToString("f2");

	}

    IEnumerator changeScence(){
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }

}
