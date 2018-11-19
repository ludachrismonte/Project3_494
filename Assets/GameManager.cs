wusing System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour 
{
    public Text win_text;
    public GameObject win_box;

    public Text[] m_Placements = new Text[4];

    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;

    private score m_P1Score;
    private score m_P2Score;
    private score m_P3Score;
    private score m_P4Score;

    private float timer;
    private bool m_GameOver = false;

    //public IEnumerator StartTheGame()
    //{

    //}

	// Use this for initialization
	void Start () {
        timer = 0;
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player3 = GameObject.FindGameObjectWithTag("Player3");
        player4 = GameObject.FindGameObjectWithTag("Player4");

        m_P1Score = player1.GetComponent<score>();
        m_P2Score = player2.GetComponent<score>();
        m_P3Score = player3.GetComponent<score>();
        m_P4Score = player4.GetComponent<score>();

        m_Placements[0].text = "#1";
        m_Placements[1].text = "#1";
        m_Placements[2].text = "#1";
        m_Placements[3].text = "#1";
    }

    public void Win(string s) 
    {
        m_GameOver = true;
        Time.timeScale = .5f;
        win_box.SetActive(true);
        win_text.text = s + " wins!";
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    public void UpdateScore()
    {
        SortedDictionary<int, List<int>> scores = new SortedDictionary<int, List<int>>();
        int p1Score = m_P1Score.GetCurrentScore();
        int p2Score = m_P2Score.GetCurrentScore();
        int p3Score = m_P3Score.GetCurrentScore();
        int p4Score = m_P4Score.GetCurrentScore();

        scores.Add(p1Score, new List<int>{ 1 });

        if (scores.ContainsKey(p2Score))
            scores[p2Score].Add(2);
        else
            scores.Add(p2Score, new List<int> { 2 });

        if (scores.ContainsKey(p3Score))
            scores[p3Score].Add(3);
        else
            scores.Add(p3Score, new List<int> { 3 });

        if (scores.ContainsKey(p4Score))
            scores[p4Score].Add(4);
        else
            scores.Add(p4Score, new List<int> { 4 });

        int place = scores.Keys.Count;

        foreach (KeyValuePair<int, List<int>> entry in scores)
        {
            foreach (int player in entry.Value)
            {
                m_Placements[player - 1].text = "#" + place;
            }
            --place;
        }
        scores.Clear();
    }
}
