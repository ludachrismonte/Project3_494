using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour 
{
    public int m_MaxTimeToWin = 300;
    public int m_MaxFireHoopPoints = 50;

    private int m_TimeToWin;
    private int m_FireHoopPointsFlag;
    private int m_FireHoopPointsNoFlag;

    private Text m_TimeToWinText;
    private Text m_FirehoopPointsFlagText;
    private Text m_FirehoopPointsNoFlagText;

    private void Start()
    {
        m_TimeToWinText = transform.Find("Time").GetComponent<Text>();
        m_FirehoopPointsFlagText = transform.Find("FireFlag").GetComponent<Text>();
        m_FirehoopPointsNoFlagText = transform.Find("FireNoFlag").GetComponent<Text>();

        m_TimeToWin = PlayerPrefs.GetInt("DemoIsland_TimeToWin");
        m_FireHoopPointsFlag = PlayerPrefs.GetInt("DemoIsland_FireHoopBonusFlag");
        m_FireHoopPointsNoFlag = PlayerPrefs.GetInt("DemoIsland_FireHoopBonusNoFlag");

        m_TimeToWinText.text = m_TimeToWin.ToString();
        m_FirehoopPointsFlagText.text = m_FireHoopPointsFlag.ToString();
        m_FirehoopPointsNoFlagText.text = m_FireHoopPointsNoFlag.ToString();
    }

    public void Inc_TimeToWin() 
    {
        if (m_TimeToWin < m_MaxTimeToWin)
        {
            m_TimeToWin += 10;
            m_TimeToWinText.text = m_TimeToWin.ToString();
        }
    }

    public void Dec_TimeToWin()
    {
        if (m_TimeToWin > 10) 
        {
            m_TimeToWin -= 10;
            m_TimeToWinText.text = m_TimeToWin.ToString();
        }
    }

    public void Inc_FirehoopPoints()
    {
        if (m_FireHoopPointsFlag < m_MaxFireHoopPoints)
        {
            m_FireHoopPointsFlag += 2;
            m_FirehoopPointsFlagText.text = m_FireHoopPointsFlag.ToString();
        }
    }

    public void Dec_FirehoopPoints()
    {
        if (m_FireHoopPointsFlag >= 2) 
        {
            m_FireHoopPointsFlag -= 2;
            m_FirehoopPointsFlagText.text = m_FireHoopPointsFlag.ToString();
        }
    }

    public void Inc_FirehoopPoints_NoFlag()
    {
        if (m_FireHoopPointsNoFlag < m_MaxFireHoopPoints)
        {
            m_FireHoopPointsNoFlag += 2;
            m_FirehoopPointsNoFlagText.text = m_FireHoopPointsNoFlag.ToString();
        }
    }

    public void Dec_FirehoopPoints_NoFlag()
    {
        if (m_FireHoopPointsNoFlag >= 2)
        {
            m_FireHoopPointsNoFlag -= 2;
            m_FirehoopPointsNoFlagText.text = m_FireHoopPointsNoFlag.ToString();
        }
    }

    public void Submit() 
    {
        MenuManager.instance.SubmitSettings(m_TimeToWin, m_FireHoopPointsFlag, m_FireHoopPointsNoFlag);
    }
}
