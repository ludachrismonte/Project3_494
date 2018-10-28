using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    public Text timer;
    public Text speedometer;
    public Text healthText;
    //public Button quit;
    public float allotedTime; //Length of round in seconds
    float startTime;
    Rigidbody carRb;
    Health health;
    string kmPerHour = "km/hr";
    //public bool changeScene = false;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
        carRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        health = GameObject.FindWithTag("Player").GetComponent<Health>();
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
        string speed = ((int)carRb.velocity.magnitude * 3.6f).ToString("f2");
        speedometer.text = speed + " " + kmPerHour;

        //Health
        healthText.text = health.health.ToString("f2");

	}

    IEnumerator changeScence(){
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }

}
