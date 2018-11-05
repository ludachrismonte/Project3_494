using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Text timer;
    public Text winner;
    public RawImage winnerBackground;
    public GameObject[] speedometer;
    //public Text[] healthText;
    //public Button quit;
    public float allotedTime; //Length of round in seconds
    float startTime;
    Rigidbody[] carRb;
    Health[] health;
    bool[] dead;
    string kmPerHour = "km/hr";
    //public bool changeScene = false;
    // Use this for initialization
    void Start()
    {
        carRb = new Rigidbody[4];
        health = new Health[4];
        dead = new bool[4];
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
        winner.enabled = false;
        winnerBackground.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            StartCoroutine(changeScence());
        }
        //if(health[0].health<=0 && health[1].health <= 0 && health[2].health <= 0 && health[3].health <= 0){
        //    StartCoroutine(changeScence()); 
        //}
        if (checkSceneStatus() != -1)
        {
            winner.enabled = true;
            winnerBackground.enabled = true;
            int playerWin = checkSceneStatus() + 1;
            winner.text = "Player " + playerWin + " Wins!";
            StartCoroutine(changeScence());
        }



        //Timer
        float timeTillEnd = allotedTime - (Time.time - startTime);
        if (timeTillEnd < 0)
        {
            timeTillEnd = 0;
            timer.color = Color.red;
            StartCoroutine(changeScence());
        }
        string minutes = ((int)timeTillEnd / 60).ToString();
        string seconds = (timeTillEnd % 60).ToString("f2");
        timer.text = minutes + ":" + seconds;

        //SpeedoMeter
        //Player1
        float speed = carRb[0].velocity.magnitude * 3.6f;
        speedometer[0].transform.eulerAngles = new Vector3(0, 0, 515 - speed);
        //Player2
        speed = carRb[1].velocity.magnitude * 3.6f;
        speedometer[1].transform.eulerAngles = new Vector3(0, 0, 515 - speed);
        //Player3
        speed = carRb[2].velocity.magnitude * 3.6f;
        speedometer[2].transform.eulerAngles = new Vector3(0, 0, 515 - speed);
        //Player4
        speed = carRb[3].velocity.magnitude * 3.6f;
        speedometer[3].transform.eulerAngles = new Vector3(0, 0, 515 - speed);
        /*
        //Health
        //Player1
        if (health[0].health > 0)
        {
            healthText[0].text = health[0].health.ToString("f2");
        }
        else
        {
            carRb[0].gameObject.SetActive(false);
            healthText[0].text = "0";
            healthText[0].color = Color.red;
            dead[0] = true;
        }
        //Player2
        if (health[1].health > 0)
        {
            healthText[1].text = health[1].health.ToString("f2");
        }
        else
        {
            carRb[1].gameObject.SetActive(false);
            healthText[1].text = "0";
            healthText[1].color = Color.red;
            dead[1] = true;
        }
        //Player3
        if (health[2].health > 0)
        {
            healthText[2].text = health[2].health.ToString("f2");
        }
        else
        {
            carRb[2].gameObject.SetActive(false);
            healthText[2].text = "0";
            healthText[2].color = Color.red;
            dead[2] = true;
        }
        //Player4
        if (health[3].health > 0)
        {
            healthText[3].text = health[3].health.ToString("f2");
        }
        else
        {
            carRb[3].gameObject.SetActive(false);
            healthText[3].text = "0";
            healthText[3].color = Color.red;
            dead[3] = true;
        }
        */


    }

    int checkSceneStatus()
    {
        int lastStanding = 0;
        int countDead = 0;
        for (int i = 0; i < 4; ++i)
        {
            if (dead[i])
            {
                countDead++;
            }
            else
            {
                lastStanding = i;
            }
        }
        if (countDead == 3)
        {
            return lastStanding;
        }
        return -1;
    }

    IEnumerator changeScence()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }

}