using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadyUpManager : MonoBehaviour 
{
    public static ReadyUpManager instance;

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
        m_P1Ready = false;
        m_P2Ready = false;
        m_P3Ready = false;
        m_P4Ready = false;
    }

    public void PlayersReadiedUp(PlayerNumber player)
    {
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
            SceneManager.LoadScene("Arena");
        }
    }

    private IEnumerator OpenArean()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Arena");
    }
}
