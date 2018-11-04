using System.Collections;
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
