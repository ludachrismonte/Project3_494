using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
    public string me = "player";
    public Image ScoreBar;
    public GameObject flag_object;
    public ObjectiveTracker m_ObjectiveTracker;

    private readonly int m_ScoreToWin = 60;
    private MeshRenderer Flag = null;
    private int current_score;
    private GameManager manager;
    private RingSwitcher Flags;
    private RingSwitcher Rings;
    private float timer;
    // Bool to keep track of if the player has the flag
    private bool hasFlag = false;

    private void Start()
    {

        Flag = transform.Find("flag").gameObject.GetComponent<MeshRenderer>();
        //Rings = GameObject.FindWithTag("FireRings").GetComponent<RingSwitcher>();
        Flags = GameObject.FindWithTag("Flags").GetComponent<RingSwitcher>();

        manager = GameManager.instance;
        current_score = 0;
        ScoreBar.fillAmount = 0.0f;
        timer = 0.0f;
    }

    private void LateUpdate()
    {
        if (hasFlag) 
        {
            timer += Time.deltaTime;
            if (timer > 1) {
                timer = 0.0f;
                add_score(1);
            }
        }
        if (current_score >= m_ScoreToWin) 
        {
            manager.Win(me);
        }
    }

    private void get_flag() 
    {
        Flag.enabled = true;
        hasFlag = true;
        switch (me)
        {
            case "player 1":
                m_ObjectiveTracker.SetFlagHolder(FlagHolder.p1);
                break;
            case "player 2":
                m_ObjectiveTracker.SetFlagHolder(FlagHolder.p2);
                break;
            case "player 3":
                m_ObjectiveTracker.SetFlagHolder(FlagHolder.p3);
                break;
            case "player 4":
                m_ObjectiveTracker.SetFlagHolder(FlagHolder.p4);
                break;
            default:
                Debug.Log("ERROR IN Score.cs: player not set");
                Application.Quit();
                break;
        }
    }

    public void lose_flag()
    {
        Flag.enabled = false;
        hasFlag = false;
        m_ObjectiveTracker.SetFlagHolder(FlagHolder.none);
    }

    private void add_score(int amt) 
    {
        current_score += amt;
        ScoreBar.fillAmount = current_score / (float)m_ScoreToWin;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string temp = collision.gameObject.tag;
        if (temp == "Player" || temp == "Player2" || temp == "Player3" || temp == "Player4") {
            Debug.Log("STEAL?");
            if (collision.relativeVelocity.magnitude > 3f) 
            {
                Debug.Log("STEAL!");
                Score other_score = collision.gameObject.GetComponent<Score>();
                if (other_score.DoesUserHaveFlag()) 
                {
                    get_flag();
                    other_score.lose_flag();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flag")
        {
            other.gameObject.SetActive(false);
            get_flag();
        }
        if (other.tag == "FireRing" && hasFlag)
        {
            lose_flag();
            Rings.Switch();
            add_score(10);
            manager.UpdateScore();
        }
    }

    public int GetCurrentScore()
    {
        return current_score;
    }

    public void DropFlag(bool reset) 
    {
        if (hasFlag) {
            lose_flag();
            if (reset)
            {
                Instantiate(flag_object, Flags.Get_Active().position, Flags.Get_Active().rotation);
            }
            else 
            {
                StartCoroutine(DropFlagAfterDelay());
            }
        }
    }

    private IEnumerator DropFlagAfterDelay()
    {
        yield return new WaitForSeconds(2);
        Instantiate(flag_object,
                    new Vector3(transform.position.x, transform.position.y + 4, transform.position.z), 
                    Flags.Get_Active().rotation);
    }

    public bool DoesUserHaveFlag()
    {
        return hasFlag;
    }
}
