using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public GameObject build_timer;
    public Text[] countdown;
    public GameObject countdown_parent;
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
    void Update () 
    {
        timer += Time.deltaTime;
        if (timer < 4) 
        {
            if (timer % 2 < 1) 
            {
                build_timer.transform.localScale += new Vector3(0.01F, 0.01F, 0);
            }
            else build_timer.transform.localScale += new Vector3(-0.01F, -0.01F, 0);
            return;
        }
        build_timer.SetActive(false);
        if (timer > 15 && timer < 19)
        {
            countdown_parent.SetActive(true);
            float time = 20 - timer;
            countdown[0].text = "Wall Drops In: " + time.ToString("#");
            countdown[1].text = "Wall Drops In: " + time.ToString("#");
            return;
        }
        countdown_parent.SetActive(false);
        if (timer > 20 && timer < 30)
        {
            wall.transform.Translate(new Vector3(0, 0, -5) * Time.deltaTime);
            return;
        }
        if (timer > 30)
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
        SceneManager.LoadScene("Menu");
    }
}
