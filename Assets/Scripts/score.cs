<<<<<<< HEAD:Assets/score.cs
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour {

    public string me = "Player";
    public GameObject[] bar;
    public int score_count = 0;

    private GameManager manager;

    private void Start()
    {
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (score_count >= 9) {
            manager.Win(me);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ScoreZone") {
            bar[score_count].SetActive(true);
            other.gameObject.GetComponent<RandomPlacement>().Move();
            score_count++;
        }
    }
}
=======
﻿using System.Collections;
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
        if (current_score >= score_to_win) {
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
>>>>>>> 852bc429e6d229721e4df3b01692a3b79b5b0e13:Assets/Scripts/score.cs
