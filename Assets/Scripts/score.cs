using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    public string me = "Player";
    public Image bar = null;
    public GameObject zone_bar = null;
    private float score_to_win = 10f;
    private float current_score;
    private Image zone_capture;
    private float progress;
    private float complete = 1;
    private GameManager manager;

    private void Start()
    {
        zone_capture = zone_bar.transform.Find("InnerBar").GetComponent<Image>();
        current_score = 0f;
        if (bar != null) { bar.fillAmount = 0f; }
        zone_bar.SetActive(false);
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (current_score >= score_to_win) 
        {
            manager.Win(me);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ScoreZone")
        {
            zone_bar.SetActive(true);
            progress = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ScoreZone")
        {
            zone_bar.SetActive(false);
            progress = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ScoreZone")
        {
            progress += Time.deltaTime;
            if (zone_capture != null) { zone_capture.fillAmount = progress / complete; }
            if (progress >= complete)
            {
                current_score++;
                bar.fillAmount = current_score / score_to_win;
                other.gameObject.GetComponent<RandomPlacement>().Move();
            }
        }
    }
}
