﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public GameObject build_timer;
    public Text[] countdown;
    public GameObject countdown_parent;
    public GameObject wall;
    public GameObject score_zone;
    public Text win_text;
    public GameObject win_box;

    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;

    private float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
        build_timer.SetActive(true);
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player3 = GameObject.FindGameObjectWithTag("Player3");
        player4 = GameObject.FindGameObjectWithTag("Player4");

        player1.GetComponent<ControllerInput>().enabled = false;
        player2.GetComponent<ControllerInput>().enabled = false;
        player3.GetComponent<ControllerInput>().enabled = false;
        player4.GetComponent<ControllerInput>().enabled = false;

    }

    // Update is called once per frame
    void Update () 
    {
        timer += Time.deltaTime;
        if (timer > 4 && timer < 5) {
            player1.GetComponent<ControllerInput>().enabled = true;
            player2.GetComponent<ControllerInput>().enabled = true;
            player3.GetComponent<ControllerInput>().enabled = true;
            player4.GetComponent<ControllerInput>().enabled = true;
        }
        if (timer > 5 && timer < 10) 
        {
            if (timer % 2 < 1) 
            {
                build_timer.transform.localScale += new Vector3(0.01F, 0.01F, 0);
            }
            else build_timer.transform.localScale += new Vector3(-0.01F, -0.01F, 0);
            return;
        }
        build_timer.SetActive(false);
        if (timer > 25 && timer < 29)
        {
            countdown_parent.SetActive(true);
            float time = 30 - timer;
            countdown[0].text = "Wall Drops In: " + time.ToString("#");
            countdown[1].text = "Wall Drops In: " + time.ToString("#");
            return;
        }

        countdown_parent.SetActive(false);

        if (timer > 30 && timer < 40)
        {
            if (!score_zone.gameObject.activeSelf)
            {
                score_zone.SetActive(true);
                score_zone.GetComponent<RandomPlacement>().Move();
            }

            wall.transform.Translate(new Vector3(0, -5, 0) * Time.deltaTime);
            return;
        }
        if (timer > 40)
        {
            Destroy(wall.gameObject);
        }
    }

    public void Win(string s) 
    {
        Time.timeScale = .5f;
        win_box.SetActive(true);
        win_text.text = s + " Wins!";
        StartCoroutine(changeScene());
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
}
