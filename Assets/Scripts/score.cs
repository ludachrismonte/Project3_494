using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    public float progress;
    public float complete;
    public string me = "Player";
    public Image bar = null;
    public Image zone_capture = null;
    private float score_to_win = 10f;
    private float current_score;

    private GameManager manager;

    private void Start()
    {
        current_score = 0f;
        if (bar != null) { bar.fillAmount = 0f; }
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (current_score >= score_to_win) 
        {
            manager.Win(me);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ScoreZone")
        {
            progress++;
            if (progress >= complete)
            {
                current_score++;
                if (zone_capture != null) { zone_capture.fillAmount = progress / complete; }
            }
            if (bar != null) { bar.fillAmount = current_score / score_to_win; }
            other.gameObject.GetComponent<RandomPlacement>().Move();
        }
    }
}
