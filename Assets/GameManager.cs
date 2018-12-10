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

    public AudioClip welcome;
    public GameObject IntroCam;
    public Transform End;
    public GameObject Demolition;
    public GameObject Island;

    private Image explosion;
    private Image wood;
    private Text main_text;

    public AudioClip blow;
    public AudioClip buzzer;
    public AudioClip horn;
    public AudioClip crash1;
    public AudioClip crash2;


    private GameObject win_box;
    private Image game_black;

    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;

    public int TimeToWin { get; private set; }
    public int FireHoopPointsFlag { get; private set; }
    public int FireHoopPointsNoFlag { get; private set; }

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
        TimeToWin = PlayerPrefs.GetInt("DemoIsland_TimeToWin");
        FireHoopPointsFlag = PlayerPrefs.GetInt("DemoIsland_FireHoopBonusFlag");
        FireHoopPointsNoFlag = PlayerPrefs.GetInt("DemoIsland_FireHoopBonusNoFlag");
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

        game_black.color = new Color(game_black.color.r, game_black.color.g, game_black.color.b, 1f);

        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player3 = GameObject.FindGameObjectWithTag("Player3");
        player4 = GameObject.FindGameObjectWithTag("Player4");

        player1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player4.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        Demolition.SetActive(false);
        Island.SetActive(false);

        // newly added
        StartCoroutine(FadeIn(game_black));
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        AudioSource.PlayClipAtPoint(welcome, IntroCam.transform.position);
        UI.GetComponent<Canvas>().enabled = false;
        yield return new WaitForSeconds(2.1f);
        AudioSource.PlayClipAtPoint(crash1, IntroCam.transform.position);
        IntroCam.GetComponent<CameraShake>().Shake(0.7f, 2.0f);
        Demolition.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        AudioSource.PlayClipAtPoint(crash2, IntroCam.transform.position);
        IntroCam.GetComponent<CameraShake>().Shake(0.7f, 2.0f);
        Island.SetActive(true);
        yield return new WaitForSeconds(2);
        float counter = 0.0f;
        float duration = 2f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            IntroCam.transform.position = Vector3.Lerp(IntroCam.transform.position, End.position, counter / duration);
            yield return null;
        }
        //IntroCam.transform.position = End.position;
        //yield return new WaitForSeconds(1);
        Destroy(IntroCam);
        UI.GetComponent<Canvas>().enabled = true;
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
        StartCoroutine(Pump());
        AudioSource.PlayClipAtPoint(buzzer, Camera.main.transform.position);
        main_text.color = Color.red;
        yield return new WaitForSeconds(2);
        main_text.text = "set...";
        StartCoroutine(Pump());
        AudioSource.PlayClipAtPoint(buzzer, Camera.main.transform.position);
        main_text.color = Color.yellow;
        yield return new WaitForSeconds(2);
        main_text.text = "go!";
        StartCoroutine(Pump());
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

    private IEnumerator Pump() {
        Vector3 initial = main_text.gameObject.transform.localScale;
        for (float i = 0f; i < .2f; i += Time.deltaTime) {
            main_text.gameObject.transform.localScale = initial * (1f + i);
            yield return null;
        }
        Vector3 peak = main_text.gameObject.transform.localScale;
        for (float i = 0f; i < .2f; i += Time.deltaTime)
        {
            main_text.gameObject.transform.localScale = peak * (1f - i);
            yield return null;
        }
        main_text.gameObject.transform.localScale = initial;
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

    public void Win(string player) 
    {
        Color playerColor = Color.white;
        switch (player)
        {
            case "player 1":
                playerColor = Color.blue;
                break;
            case "player 2":
                playerColor = Color.red; 
                break;
            case "player 3":
                playerColor = Color.green;
                break;
            case "player 4":
                playerColor = Color.yellow;
                break;
            default:
                Debug.Log("ERROR IN Score.cs: player not set");
                Application.Quit();
                break;
        }
        win_box.SetActive(true);
        win_box.GetComponent<Image>().color = playerColor;
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
}
