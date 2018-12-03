using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PlayerNumber { one, two, three, four };

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;
    public GameObject UI;
    public GameObject GameSettings;
    public Text[] m_Placements = new Text[4];

    private Image explosion;
    private Image wood;
    private Text main_text;

    public AudioClip blow;
    public AudioClip buzzer;
    public AudioClip horn;

    private GameObject win_box;
    private Image game_black;
    private Image menu_black;

    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;

    private Score m_P1Score;
    private Score m_P2Score;
    private Score m_P3Score;
    private Score m_P4Score;

	private void Awake () 
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

    private void Start()
    {
        explosion = UI.transform.Find("Explosion").GetComponent<Image>();
        wood = UI.transform.Find("Wood").GetComponent<Image>();
        wood.transform.localScale = Vector3.zero;
        explosion.transform.localScale = Vector3.zero;
        wood.gameObject.SetActive(false);
        explosion.gameObject.SetActive(false);

        main_text = UI.transform.Find("MainText").GetComponent<Text>();
        main_text.text = "";

        win_box = UI.transform.Find("WinBox").gameObject;
        game_black = UI.transform.Find("black").GetComponent<Image>();
        menu_black = GameSettings.transform.Find("black").GetComponent<Image>();

        game_black.color = new Color(game_black.color.r, game_black.color.g, game_black.color.b, 1f);
        menu_black.color = new Color(menu_black.color.r, menu_black.color.g, menu_black.color.b, 1f);
        GameSettings.SetActive(true);
        StartCoroutine(FadeIn(menu_black));

        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player3 = GameObject.FindGameObjectWithTag("Player3");
        player4 = GameObject.FindGameObjectWithTag("Player4");

        m_P1Score = player1.GetComponent<Score>();
        m_P2Score = player2.GetComponent<Score>();
        m_P3Score = player3.GetComponent<Score>(); 
        m_P4Score = player4.GetComponent<Score>();

        foreach (Text text in m_Placements)
            text.text = "";

        player1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player4.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public void MenuSubmit(int score, int FirehoopPoints, int FirehoopPoints_NoFlag) 
    {
        GameSettings.SetActive(false);
        player1.GetComponent<Score>().SetGameScore(score, FirehoopPoints, FirehoopPoints_NoFlag);
        player2.GetComponent<Score>().SetGameScore(score, FirehoopPoints, FirehoopPoints_NoFlag);
        player3.GetComponent<Score>().SetGameScore(score, FirehoopPoints, FirehoopPoints_NoFlag);
        player4.GetComponent<Score>().SetGameScore(score, FirehoopPoints, FirehoopPoints_NoFlag);
        StartCoroutine(FadeIn(game_black));
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1);
        wood.gameObject.SetActive(true);
        explosion.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(blow, Camera.main.transform.position);
        for (float i = 0f; i < 0.5f; i += Time.deltaTime)
        {
            wood.transform.localScale = Vector3.one * i * 2;
            explosion.transform.localScale = Vector3.one * i * 2;
            yield return null;
        }
        wood.transform.localScale = Vector3.one;
        explosion.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(.5f);

        main_text.text = "ready...";
        AudioSource.PlayClipAtPoint(buzzer, Camera.main.transform.position);
        main_text.color = Color.red;
        yield return new WaitForSeconds(2);
        main_text.text = "set...";
        AudioSource.PlayClipAtPoint(buzzer, Camera.main.transform.position);
        main_text.color = Color.yellow;
        yield return new WaitForSeconds(2);
        main_text.text = "go!";
        AudioSource.PlayClipAtPoint(horn, Camera.main.transform.position);
        main_text.color = Color.green;

        player1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player4.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        yield return new WaitForSeconds(2);
        main_text.text = "";

        for (float i = 0.5f; i > 0; i -= Time.deltaTime) {
            wood.transform.localScale = Vector3.one * i * 2;
            explosion.transform.localScale = Vector3.one * i * 2;
            yield return null;
        }
        wood.gameObject.SetActive(false);
        explosion.gameObject.SetActive(false);
    }

    private IEnumerator FadeIn(Image black) 
    {
        for (float i = 2f; i > 0f; i -= Time.deltaTime)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, i/2);
            yield return null;
        }
        black.gameObject.SetActive(false);
    }

    public void Win(string s) 
    {
        win_box.SetActive(true);
        //win_text.text = "game over";
        //win_text.color = Color.yellow;
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        Time.timeScale = .5f;
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1.0f;

        // Keep users cars for post game screen 
        DontDestroyOnLoad(player1);
        DontDestroyOnLoad(player2);
        DontDestroyOnLoad(player3);
        DontDestroyOnLoad(player4);

        SceneManager.LoadScene("PostGame");
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
                switch (place)
                {
                    case 1:
                        m_Placements[player - 1].color = Color.yellow;
                        break;
                    case 2:
                        m_Placements[player - 1].color = Color.magenta;
                        break;
                    case 3:
                        m_Placements[player - 1].color = Color.blue;
                        break;
                    case 4:
                        m_Placements[player - 1].color = Color.green;
                        break;
                    default:
                        Debug.LogError("ERROR: Placement is out of range [1,4].");
                        Application.Quit();
                        break;
                }
                m_Placements[player - 1].text = "#" + place;
            }
            --place;
        }
        scores.Clear();
    }
}
