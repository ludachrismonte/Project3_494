using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    public string me = "player";
    public Image bar = null;
    public GameObject zone_bar = null;
    public int m_ScoreToWin = 10;

    private int current_score;
    private Image zone_capture;
    private float progress;
    private float complete = 1;
    private GameManager manager;
    private bool can_score;

    // Reference to the actual scorezone
    public GameObject ScoreZone;
    public GameObject FlagZone;

    // Bool to keep track of if the player has the flag
    private bool hasFlag = false;

    private void Start()
    {
        zone_capture = zone_bar.transform.Find("InnerBar").GetComponent<Image>();
        current_score = 0;
        if (bar != null) { bar.fillAmount = 0f; }
        zone_bar.SetActive(false);
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (current_score >= m_ScoreToWin) 
        {
            manager.Win(me);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        can_score = true;
        if ((other.tag == "ScoreZone") || (other.tag == "FlagZone"))
        {
            zone_bar.SetActive(true);
            progress = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "ScoreZone") || (other.tag == "FlagZone"))
        {
            zone_bar.SetActive(false);
            progress = 0;
            can_score = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        // If the player is colliding with the scorezone
        if ((other.tag == "ScoreZone") && (hasFlag))
        {
            progress += Time.deltaTime;

            // Fill UI bar
            if (zone_capture != null) { zone_capture.fillAmount = progress / complete; }

            // Add to players score if necessary
            if (progress >= complete && can_score)
            {
                current_score++;
                can_score = false;
                bar.fillAmount = current_score / (float)m_ScoreToWin;
                ScoreZone.SetActive(false);
                FlagZone.SetActive(true);
                //other.gameObject.GetComponent<RandomPlacement>().Move();
                manager.UpdateScore();

                // Reset flag
                hasFlag = false;

                // CHange arrow
                GetComponentInChildren<LookAtObject>().target = FlagZone;

                // Reset values, since triggerexit isn't called
                zone_bar.SetActive(false);
                progress = 0;
                can_score = false;
            }
        }

        else if (other.tag == "FlagZone"){

            progress += Time.deltaTime;

            // Fill UI bar
            if (zone_capture != null) { zone_capture.fillAmount = progress / complete; }

            // Add to players score if necessary
            if (progress >= complete && can_score)
            {
                can_score = false;
                FlagZone.GetComponent<RandomPlacement>().Move();
                FlagZone.SetActive(false);
                ScoreZone.SetActive(true);
                //Debug.Log("Deactivated flag zone");
                // Collect flag
                hasFlag = true;

                // CHange arrow
                GetComponentInChildren<LookAtObject>().target = ScoreZone;

                // Reset values, since triggerexit isn't called
                zone_bar.SetActive(false);
                progress = 0;
                can_score = false;
            }
        }
    }

    public int GetCurrentScore()
    {
        return current_score;
    }
}
