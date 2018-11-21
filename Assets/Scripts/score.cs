using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour 
{
    public string me = "player";
    public int m_ScoreToWin = 10;
    public Image ScoreBar;
    public GameObject flag_object;
    public ObjectiveTracker m_ObjectiveTracker;

    private MeshRenderer Flag = null;
    private int current_score;
    private GameManager manager;
    private RingSwitcher Rings;
    private RingSwitcher Flags;

    // Bool to keep track of if the player has the flag
    private bool hasFlag = false;

    private void Start()
    {
        Flag = transform.Find("flag").gameObject.GetComponent<MeshRenderer>();
        Rings = GameObject.FindWithTag("FireRings").GetComponent<RingSwitcher>();
        Flags = GameObject.FindWithTag("Flags").GetComponent<RingSwitcher>();
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        current_score = 0;
        ScoreBar.fillAmount = 0.0f;
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
            other.gameObject.SetActive(false);
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
        if (other.tag == "FireRing" && hasFlag)
        {
            Rings.Switch();
            Flags.Switch();

            Flag.enabled = false;

            current_score++;

            ScoreBar.fillAmount = (float)current_score / (float)m_ScoreToWin;

            manager.UpdateScore();

            hasFlag = false;
            m_ObjectiveTracker.SetFlagHolder(FlagHolder.none);
        }
    }

    public int GetCurrentScore()
    {
        return current_score;
    }

    public void DropFlag(bool reset) 
    {
        Flag.enabled = false;
        if (hasFlag) {
            hasFlag = false;
            m_ObjectiveTracker.SetFlagHolder(FlagHolder.none);
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
