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
            PlayerPrefs.SetInt("DemoIsland_TimeToWin", Constants.GameSettingsDefaults.TimeToWin);
        }

        if (!PlayerPrefs.HasKey("DemoIsland_FireHoopBonusFlag"))
        {
            PlayerPrefs.SetInt("DemoIsland_FireHoopBonusFlag", Constants.GameSettingsDefaults.FireHoopBonusFlag);
        }

        if (!PlayerPrefs.HasKey("DemoIsland_FireHoopBonusNoFlag"))
        {
            PlayerPrefs.SetInt("DemoIsland_FireHoopBonusNoFlag", Constants.GameSettingsDefaults.FireHoopBonusNoFlag);
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
        m_SettingsUI.SetActive(true);
        m_MainMenuUI.SetActive(false);
        m_SettingsUI.GetComponent<GameSettings>().m_FirstButton.Select();
    }

    public void SubmitSettings(int timeToWin, int fireHoopPointsFlag, int fireHoopPointsNoFlag)
    {
        m_SettingsUI.SetActive(false);
        m_MainMenuUI.SetActive(true);

        PlayerPrefs.SetInt("DemoIsland_TimeToWin", timeToWin);
        PlayerPrefs.SetInt("DemoIsland_FireHoopBonusFlag", fireHoopPointsFlag);
        PlayerPrefs.SetInt("DemoIsland_FireHoopBonusNoFlag", fireHoopPointsNoFlag);
        PlayerPrefs.Save();

        black.gameObject.SetActive(false);
        m_StartButton.Select();
    }

    public void OpenMainMenu()
    {
        m_SettingsUI.SetActive(false);
        m_MainMenuUI.SetActive(true);
        m_StartButton.Select();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
