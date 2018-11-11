using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public GameObject m_StartBuilding;
    public Text m_WallDropsText;
    public GameObject wall;
    public GameObject score_zone;
    public Text win_text;
    public GameObject win_box;

    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;

    private float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
        m_StartBuilding.SetActive(true);
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player3 = GameObject.FindGameObjectWithTag("Player3");
        player4 = GameObject.FindGameObjectWithTag("Player4");

        player1.GetComponent<ControllerInput>().enabled = false;
        player2.GetComponent<ControllerInput>().enabled = false;
        player3.GetComponent<ControllerInput>().enabled = false;
        player4.GetComponent<ControllerInput>().enabled = false;
    }

    // Update is called once per frame
    void Update () 
    {
        timer += Time.deltaTime;
        if (timer < 5) {
            if (timer > 4) {
                player1.GetComponent<ControllerInput>().enabled = true;
                player2.GetComponent<ControllerInput>().enabled = true;
                player3.GetComponent<ControllerInput>().enabled = true;
                player4.GetComponent<ControllerInput>().enabled = true;
            }
            return;
        }
        if (timer > 5 && timer < 10) 
        {
            if (timer % 2 < 1) 
            {
                m_StartBuilding.transform.localScale += new Vector3(0.01F, 0.01F, 0);
            }
            else m_StartBuilding.transform.localScale += new Vector3(-0.01F, -0.01F, 0);
            return;
        }

        m_StartBuilding.SetActive(false);

        if (timer > 25 && timer < 30)
        {
            m_WallDropsText.gameObject.SetActive(true);
            int time = 30 - (int)timer;
            m_WallDropsText.text = "wall drops in: " + time.ToString();
            return;
        }

        m_WallDropsText.gameObject.SetActive(false);

        if (timer > 30 && timer < 40)
        {
            if (!score_zone.gameObject.activeSelf)
            {
                score_zone.SetActive(true);
                score_zone.GetComponent<RandomPlacement>().Move();
            }

            wall.transform.Translate(new Vector3(0, -5, 0) * Time.deltaTime);
            return;
        }
        if (timer > 40)
        {
            Destroy(wall.gameObject);
        }
    }

    public void Win(string s) 
    {
        Time.timeScale = .5f;
        win_box.SetActive(true);
        win_text.text = s + " Wins!";
        StartCoroutine(changeScene());
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
}
