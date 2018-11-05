using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    public string me = "Player";
    public Image bar;
    private float score_to_win = 10f;
    private float current_score;

    private GameManager manager;

    private void Start()
    {
        current_score = 0f;
        bar.fillAmount = 0f;
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
        if (other.tag == "ScoreZone") {
            current_score++;
            bar.fillAmount = current_score / score_to_win;
            other.gameObject.GetComponent<RandomPlacement>().Move();
        }
    }
}
