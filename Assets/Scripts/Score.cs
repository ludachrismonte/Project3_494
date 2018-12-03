using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
    public string me = "player";
    public Image ScoreBar;
    public Image ScoreBar2;
    public Image ScoreBar3;
    public Image ScoreBar4;
    public Image ScoreBar5;
    public Image ScoreBar6;
    public Image ScoreBar7;
    public Image ScoreBar8;

    public GameObject flag_object;
    private ObjectiveTracker m_ObjectiveTracker;
    private int m_ScoreToWin = 10000;
    private GameObject Flag = null;
    private int current_score;
    private GameManager manager;
    private RingSwitcher Flags;
    private RingSwitcher Rings;
    private float timer;
    // Bool to keep track of if the player has the flag
    private bool hasFlag = false;

    bool canLoseFlag = true;

    public void SetGameScore(int s)
    {
        m_ScoreToWin = s;
    }

    private void Start()
    {
        m_ObjectiveTracker = GameManager.instance.GetComponent<ObjectiveTracker>();
        Flag = transform.Find("flag").gameObject;
        Flag.SetActive(false);
        Rings = GameObject.FindWithTag("FireRings").GetComponent<RingSwitcher>();
        Flags = GameObject.FindWithTag("Flags").GetComponent<RingSwitcher>();

        manager = GameManager.instance;
        current_score = 0;
        ScoreBar.fillAmount = 0.0f;
        ScoreBar2.fillAmount = 0.0f;
        ScoreBar3.fillAmount = 0.0f;
        ScoreBar4.fillAmount = 0.0f;
        ScoreBar5.fillAmount = 0.0f;
        ScoreBar6.fillAmount = 0.0f;
        ScoreBar7.fillAmount = 0.0f;
        ScoreBar8.fillAmount = 0.0f;

        ScoreBar.enabled = false;
        ScoreBar2.enabled = false;
        ScoreBar3.enabled = false;
        ScoreBar4.enabled = false;
        ScoreBar5.enabled = false;
        ScoreBar6.enabled = false;
        ScoreBar7.enabled = false;
        ScoreBar8.enabled = false;

        timer = 0.0f;
    }

    private void LateUpdate()
    {
        if (hasFlag) 
        {
            ScoreBar.enabled = true;
            ScoreBar2.enabled = true;
            ScoreBar3.enabled = true;
            ScoreBar4.enabled = true;
            ScoreBar5.enabled = true;
            ScoreBar6.enabled = true;
            ScoreBar7.enabled = true;
            ScoreBar8.enabled = true;
            timer += Time.deltaTime;
            if (timer > 1) {
                timer = 0.0f;
                add_score(1);
            }
        }
        else if (!hasFlag)
        {
            ScoreBar.enabled = false;
            ScoreBar2.enabled = false;
            ScoreBar3.enabled = false;
            ScoreBar4.enabled = false;
            ScoreBar5.enabled = false;
            ScoreBar6.enabled = false;
            ScoreBar7.enabled = false;
            ScoreBar8.enabled = false;
        }
        if (current_score >= m_ScoreToWin) 
        {
            manager.Win(me);
        }
    }

    private void get_flag() 
    {
        manager.GetComponent<Announcer>().Trigger(me);
        Flag.SetActive(true);
        hasFlag = true;

        Debug.Log("Getting flag for: " + me);

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
        Flag.SetActive(false);
        hasFlag = false;
        m_ObjectiveTracker.SetFlagHolder(FlagHolder.none);
    }

    private void add_score(int amt) 
    {
        current_score += amt;
        manager.UpdateScore();
        ScoreBar.fillAmount = current_score / (float)m_ScoreToWin;
        ScoreBar2.fillAmount = ScoreBar.fillAmount;
        ScoreBar3.fillAmount = ScoreBar.fillAmount;
        ScoreBar4.fillAmount = ScoreBar.fillAmount;
        ScoreBar5.fillAmount = ScoreBar.fillAmount;
        ScoreBar6.fillAmount = ScoreBar.fillAmount;
        ScoreBar7.fillAmount = ScoreBar.fillAmount;
        ScoreBar8.fillAmount = ScoreBar.fillAmount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string temp = collision.gameObject.tag;
        if (temp == "Player" || temp == "Player2" || temp == "Player3" || temp == "Player4") {
            if (collision.relativeVelocity.magnitude > 5f) 
            {
                Score other_score = collision.gameObject.GetComponent<Score>();
                if (other_score.DoesUserHaveFlag() && other_score.canLoseFlag) 
                {
                    other_score.lose_flag();
                    get_flag();
                    StartCoroutine(FlagStealCooldown());
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Dropped Flag") {
            Destroy(other.gameObject);
            get_flag();
        }
        else if (other.tag == "Flag")
        {
            Flags.Disable_Active();
            get_flag();
        }
        if (other.tag == "FireRing" && hasFlag)
        {
            Rings.Switch();
            add_score(10);
        }
    }

    public int GetCurrentScore()
    {
        return current_score;
    }

    public int GetWinScore()
    {
        return m_ScoreToWin;
    }

    public bool DoesUserHaveFlag()
    {
        return hasFlag;
    }

    public void DropFlag(bool reset) 
    {
        if (hasFlag) {
            lose_flag();
            if (reset)
            {
                Flags.Enable_Active();
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
        GameObject NewFlag = (GameObject)Instantiate(flag_object,
                    new Vector3(transform.position.x, transform.position.y + 4, transform.position.z), 
                    Flags.Get_Active().rotation);
        NewFlag.name = "Dropped Flag";
    }

    private IEnumerator FlagStealCooldown(){
        canLoseFlag = false;
        yield return new WaitForSeconds(0.2f);
        canLoseFlag = true;
    }
}
