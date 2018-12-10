using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
    public static MenuManager instance;
    public GameObject m_MainMenuUI;
    public GameObject m_SettingsUI;
    public Button m_StartButton;

    public RawImage black;

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

        if (!PlayerPrefs.HasKey("DemoIsland_TimeToWin"))
        {
            PlayerPrefs.SetInt("DemoIsland_TimeToWin", 120);
        }

        if (!PlayerPrefs.HasKey("DemoIsland_FireHoopBonusFlag"))
        {
            PlayerPrefs.SetInt("DemoIsland_FireHoopBonusFlag", 10);
        }

        if (!PlayerPrefs.HasKey("DemoIsland_FireHoopBonusNoFlag"))
        {
            PlayerPrefs.SetInt("DemoIsland_FireHoopBonusNoFlag", 5);
        }
    }

    void Start () 
    {
        m_StartButton.Select();
    }

    public void LoadScene(string scene)
    {
        black.gameObject.SetActive(true);
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, i);
        }
        SceneManager.LoadScene(scene);
    }

    public void OpenSettings()
    {
        black.gameObject.SetActive(true);
        m_SettingsUI.SetActive(true);
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, i);
        }
        m_MainMenuUI.SetActive(false);
    }

    public void SubmitSettings(int timeToWin, int fireHoopPointsFlag, int fireHoopPointsNoFlag)
    {
        m_SettingsUI.SetActive(false);
        m_MainMenuUI.SetActive(true);

        PlayerPrefs.SetInt("DemoIsland_TimeToWin", timeToWin);
        PlayerPrefs.SetInt("DemoIsland_FireHoopBonusFlag", fireHoopPointsFlag);
        PlayerPrefs.SetInt("DemoIsland_FireHoopBonusNoFlag", fireHoopPointsNoFlag);
        PlayerPrefs.Save();

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, i);
        }
    }
}
