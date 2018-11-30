using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

    private int TimeToWin = 120;
    private int FirehoopPoints = 20;
    private int FirehoopPoints_NoFlag = 10;
    private Text TimeToWinText;
    private Text FirehoopPointsText;
    private Text FirehoopPoints_NoFlagText;
    private GameManager manager;

    private void Start()
    {
        manager = GameManager.instance;
        TimeToWinText = transform.Find("Time").GetComponent<Text>();
        FirehoopPointsText = transform.Find("FireFlag").GetComponent<Text>();
        FirehoopPoints_NoFlagText = transform.Find("FireNoFlag").GetComponent<Text>();
        TimeToWinText.text = TimeToWin.ToString();
        FirehoopPointsText.text = FirehoopPoints.ToString();
        FirehoopPoints_NoFlagText.text = FirehoopPoints_NoFlag.ToString();
    }

    public void Inc_TimeToWin() {
        TimeToWin += 10;
        TimeToWinText.text = TimeToWin.ToString();
    }

    public void Dec_TimeToWin()
    {
        if (TimeToWin > 10) {
            TimeToWin -= 10;
        }
        TimeToWinText.text = TimeToWin.ToString();
    }

    public void Inc_FirehoopPoints()
    {
        FirehoopPoints += 2;
        FirehoopPointsText.text = FirehoopPoints.ToString();
    }

    public void Dec_FirehoopPoints()
    {
        if (FirehoopPoints >= 2) {
            FirehoopPoints -= 2;
        }
        FirehoopPointsText.text = FirehoopPoints.ToString();
    }

    public void Inc_FirehoopPoints_NoFlag()
    {
        FirehoopPoints_NoFlag += 2;
        FirehoopPoints_NoFlagText.text = FirehoopPoints_NoFlag.ToString();
    }

    public void Dec_FirehoopPoints_NoFlag()
    {
        if (FirehoopPoints_NoFlag >= 2)
        {
            FirehoopPoints_NoFlag -= 2;
        }
        FirehoopPoints_NoFlagText.text = FirehoopPoints_NoFlag.ToString();
    }

    public void Submit() {
        manager.MenuSubmit(TimeToWin);
    }
}
