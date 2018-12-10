using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarSystem : MonoBehaviour {

    private GameObject Mine;
    private GameObject Other;

    private Image MyBar;
    private Image MyStaticBar;
    private Image MyNumberBar;
    private Text MyText;
    private Image OtherFlexBar;
    private Image OtherStaticBar;
    private Image OtherNumberBar;
    private Text OtherText;

    public Score MyScore;
    public Score OtherScore1;
    public Score OtherScore2;
    public Score OtherScore3;

    private int ScoreToWin;

    private int MyS1;
    private int OtherS1;
    private int OtherS2;
    private int OtherS3;

    private bool ImOnTop;

    // Use this for initialization
    void Start () {
        Mine = transform.Find("Mine").gameObject;
        MyBar = Mine.transform.Find("MyFlexBar").gameObject.GetComponent<Image>();
        MyStaticBar = Mine.transform.Find("MyStaticBar").gameObject.GetComponent<Image>();
        MyNumberBar = Mine.transform.Find("MyNumberBar").gameObject.GetComponent<Image>();
        MyText = Mine.transform.Find("Text").gameObject.GetComponent<Text>();
        Other = transform.Find("Other").gameObject;
        OtherFlexBar = Other.transform.Find("OtherFlexBar").gameObject.GetComponent<Image>();
        OtherStaticBar = Other.transform.Find("OtherStaticBar").gameObject.GetComponent<Image>();
        OtherNumberBar = Other.transform.Find("OtherNumberBar").gameObject.GetComponent<Image>();
        OtherText = Other.transform.Find("Text").gameObject.GetComponent<Text>();

        ScoreToWin = GameManager.instance.TimeToWin;
        ImOnTop = true;

        OtherFlexBar.fillAmount = 0.0f;
        MyBar.fillAmount = 0.0f;

        if (MyScore.gameObject.tag == "Player")
        {
            MyBar.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.5f);
            MyStaticBar.color = Color.blue;
            MyNumberBar.color = Color.blue;
        }
        else if (MyScore.gameObject.tag == "Player2")
        {
            MyBar.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
            MyStaticBar.color = Color.red;
            MyNumberBar.color = Color.red;
        }
        else if (MyScore.gameObject.tag == "Player3")
        {
            MyBar.color = new Color(Color.green.r, Color.green.g, Color.green.b, 0.5f);
            MyStaticBar.color = Color.green;
            MyNumberBar.color = Color.green;
        }
        else if (MyScore.gameObject.tag == "Player4")
        {
            MyBar.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.5f);
            MyStaticBar.color = Color.yellow;
            MyNumberBar.color = Color.yellow;
        }
    }

	// Update is called once per frame
	void Update () {
        MyS1 = MyScore.GetCurrentScore();
        OtherS1 = OtherScore1.GetCurrentScore();
        OtherS2 = OtherScore2.GetCurrentScore();
        OtherS3 = OtherScore3.GetCurrentScore();

        if (MyS1 > ScoreToWin || OtherS1 > ScoreToWin || OtherS2 > ScoreToWin || OtherS3 > ScoreToWin) {
            return;
        }

        MyText.text = MyS1.ToString();
        MyBar.fillAmount = (float)MyS1 / (float)ScoreToWin;

        //if im first
        if (MyS1 >= OtherS1 && MyS1 >= OtherS2 && MyS1 >= OtherS3 && !ImOnTop) {
            StartCoroutine(Swap());
            ImOnTop = true;
        }
        if ((MyS1 < OtherS1 || MyS1 < OtherS2 || MyS1 < OtherS3) && ImOnTop)
        {
            StartCoroutine(Swap());
            ImOnTop = false;
        }

        if (OtherS1 >= OtherS2 && OtherS1 >= OtherS3)
        {
            UpdateOtherColor(OtherScore1.gameObject);
            OtherText.text = OtherS1.ToString();
            OtherFlexBar.fillAmount = (float)OtherS1 / (float)ScoreToWin;
        }
        else if (OtherS2 >= OtherS1 && OtherS2 >= OtherS3)
        {
            UpdateOtherColor(OtherScore2.gameObject);
            OtherText.text = OtherS2.ToString();
            OtherFlexBar.fillAmount = (float)OtherS2 / (float)ScoreToWin;
        }
        else
        {
            UpdateOtherColor(OtherScore3.gameObject);
            OtherText.text = OtherS3.ToString();
            OtherFlexBar.fillAmount = (float)OtherS3 / (float)ScoreToWin;
        }
    }

    private void UpdateOtherColor(GameObject who) {
        if (who.tag == "Player") {
            OtherFlexBar.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.5f);
            OtherStaticBar.color = Color.blue;
            OtherNumberBar.color = Color.blue;
        }
        else if (who.tag == "Player2")
        {
            OtherFlexBar.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
            OtherStaticBar.color = Color.red;
            OtherNumberBar.color = Color.red;
        }
        else if (who.tag == "Player3")
        {
            OtherFlexBar.color = new Color(Color.green.r, Color.green.g, Color.green.b, 0.5f);
            OtherStaticBar.color = Color.green;
            OtherNumberBar.color = Color.green;
        }
        else if (who.tag == "Player4")
        {
            OtherFlexBar.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.5f);
            OtherStaticBar.color = Color.yellow;
            OtherNumberBar.color = Color.yellow;
        }
    }

    private IEnumerator Swap() {
        Vector3 MyLoc = Mine.transform.position;
        Vector3 OtherLoc = Other.transform.position;
        float counter = 0.0f;
        float duration = 0.75f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            Mine.transform.position = Vector3.Lerp(Mine.transform.position, OtherLoc, counter / duration);
            Other.transform.position = Vector3.Lerp(Other.transform.position, MyLoc, counter / duration);
            yield return null;
        }
    }

}
