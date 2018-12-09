using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadyUpManager : MonoBehaviour 
{
    public static ReadyUpManager instance;

    public string m_SceneToLoad;

    private bool m_P1Ready;
    private bool m_P2Ready;
    private bool m_P3Ready;
    private bool m_P4Ready;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start () 
    {
        if (m_SceneToLoad == null)
        {
            m_SceneToLoad = "Menu";
        }

        m_P1Ready = false;
        m_P2Ready = false;
        m_P3Ready = false;
        m_P4Ready = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(m_SceneToLoad);
        }
    }

    public void PlayersReadiedUp(PlayerNumber player, PlayerTutorialManager playerTutorialManager)
    {
        playerTutorialManager.ReadyUp();

        switch (player)
        {
            case PlayerNumber.one:
                m_P1Ready = true;
                break;
            case PlayerNumber.two:
                m_P2Ready = true;
                break;
            case PlayerNumber.three:
                m_P3Ready = true;
                break;
            case PlayerNumber.four:
                m_P4Ready = true;
                break;
        }

        if (m_P1Ready && m_P2Ready && m_P3Ready && m_P4Ready)
        {
            StartCoroutine(OpenArena());
        }
    }

    private IEnumerator OpenArena()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(m_SceneToLoad);
    }
}
