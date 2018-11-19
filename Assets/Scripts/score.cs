using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    public string me = "player";
    public int m_ScoreToWin = 10;
    private MeshRenderer Flag = null;

    private int current_score;
    private GameManager manager;

    // Bool to keep track of if the player has the flag
    private bool hasFlag = false;

    private void Start()
    {
        Flag = transform.Find("flag").gameObject.GetComponent<MeshRenderer>();
        current_score = 0;
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
        if (other.tag == "Flag")
        {
            Destroy(other.gameObject);
            Flag.enabled = true;
            hasFlag = true;
        }
        if (other.tag == "FireRing" && hasFlag){
            Flag.enabled = false;
            current_score++;
            manager.UpdateScore();

            hasFlag = false;
        }
    }

    public int GetCurrentScore()
    {
        return current_score;
    }
}
