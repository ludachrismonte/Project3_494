using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public GameObject build_timer;
    public Text countdown;
    public Text countdown_parent;
    public GameObject wall;
    public Text win_text;
    public GameObject win_box;


    private float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
        build_timer.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer < 6) {
            if (timer % 2 < 1) {
                build_timer.transform.localScale += new Vector3(0.01F, 0.01F, 0);
            }
            else build_timer.transform.localScale += new Vector3(-0.01F, -0.01F, 0);
            return;
        }
        build_timer.SetActive(false);
        if (timer > 15)
        {
            float time = 20 - timer;
            countdown.text = "Wall Drops In: " + time.ToString();
        }
        if (timer > 20)
        {
            wall.transform.Translate(new Vector3(0, 0, -5) * Time.deltaTime);
        }
        if (timer > 30)
        {
            Destroy(wall.gameObject);
        }
    }

    public void Win(string s) {
        Time.timeScale = .5f;
        win_box.SetActive(true);
        win_text.text = s + " Wins!";
        StartCoroutine(changeScene());
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
