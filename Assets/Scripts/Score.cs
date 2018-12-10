using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
    public string me = "player";
    public GameObject flag_object;
    public float m_FlagStealCooldown = 1f;

    private ObjectiveTracker m_ObjectiveTracker;
    private GameObject Flag = null;
    private int current_score;
    private GameManager manager;
    private FlagManager m_FlagManager;
    private FireRingManager m_FireRingsManager;
    private float timer;
    // Bool to keep track of if the player has the flag
    private bool hasFlag = false;

    bool canLoseFlag = true;

    private int m_ScoreToWin;
    private int m_FireHoopFlag;
    private int m_FireHoopNoFlag;

    private void Start()
    {
        m_ObjectiveTracker = GameManager.instance.GetComponent<ObjectiveTracker>();
        Flag = transform.Find("OnCarFlag").gameObject;
        Flag.SetActive(false);
        m_FireRingsManager = GameObject.FindWithTag("FireRings").GetComponent<FireRingManager>();
        m_FlagManager = GameObject.FindWithTag("Flags").GetComponent<FlagManager>();

        manager = GameManager.instance;
        current_score = 0;
        timer = 0.0f;

        m_ScoreToWin = GameManager.instance.TimeToWin;
        m_FireHoopFlag = GameManager.instance.FireHoopPointsFlag;
        m_FireHoopNoFlag = GameManager.instance.FireHoopPointsNoFlag;
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
        manager.GetComponent<Announcer>().Trigger(me);
        Flag.SetActive(true);

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
        Flag.SetActive(false);
        hasFlag = false;
        m_ObjectiveTracker.SetFlagHolder(FlagHolder.none);
    }

    private void add_score(int amt) 
    {
        current_score += amt;
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
            m_FlagManager.Disable_Active();
            get_flag();
        }
        if (other.tag == "FireRing" && hasFlag)
        {
            other.gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(m_FireRingsManager.FireRingHit(other.gameObject));
            add_score(m_FireHoopFlag);
        }
        else if (other.tag == "FireRing" && !hasFlag)
        {
            other.gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(m_FireRingsManager.FireRingHit(other.gameObject));
            add_score(m_FireHoopNoFlag);
        }
    }

    public int GetCurrentScore()
    {
        return current_score;
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
                manager.GetComponent<Announcer>().TriggerReset();
                m_FlagManager.Enable_Active();
            }
            else 
            {
                manager.GetComponent<Announcer>().TriggerDrop(me);
                StartCoroutine(DropFlagAfterDelay());
            }
        }
    }

    private IEnumerator DropFlagAfterDelay()
    {
        Vector3 position = transform.position;
        yield return new WaitForSeconds(2);
        GameObject NewFlag = Instantiate(flag_object,
                    new Vector3(position.x, position.y + 4, position.z),
                    m_FlagManager.Get_Active().rotation);
        NewFlag.name = "Dropped Flag";
    }

    private IEnumerator FlagStealCooldown()
    {
        canLoseFlag = false;
        yield return new WaitForSeconds(m_FlagStealCooldown);
        canLoseFlag = true;
    }
}
