using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public GameObject wheels;
    public GameObject bodies;
    public GameObject engines;
    public GameObject rockets;
    public GameObject ctf;
    public OpenDoor Left;
    public OpenDoor Right;
    public GameObject UIObjects;

    private Text t1;
    private Text t2;
    private Text t3;
    private Text t4;

    private PlayerPickup p1;
    private PlayerPickup p2;
    private PlayerPickup p3;
    private PlayerPickup p4;

    private float timer;

    private string current;

    private void Start()
    {
        wheels.SetActive(false);
        bodies.SetActive(false);
        engines.SetActive(false);
        rockets.SetActive(false);
        ctf.SetActive(false);

        t1 = UIObjects.transform.Find("p1").GetComponent<Text>();
        t2 = UIObjects.transform.Find("p2").GetComponent<Text>();
        t3 = UIObjects.transform.Find("p3").GetComponent<Text>();
        t4 = UIObjects.transform.Find("p4").GetComponent<Text>();

        p1 = GameObject.FindWithTag("Player").GetComponent<PlayerPickup>();
        p2 = GameObject.FindWithTag("Player2").GetComponent<PlayerPickup>();
        p3 = GameObject.FindWithTag("Player3").GetComponent<PlayerPickup>();
        p4 = GameObject.FindWithTag("Player4").GetComponent<PlayerPickup>();
        current = "go";
        set_text("a = forward, x = reverse");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (current == "go" && timer > 1) {
            if (p1.gameObject.GetComponent<Rigidbody>().velocity.x > 0.5f) { t1.text = ""; }
            if (p2.gameObject.GetComponent<Rigidbody>().velocity.x > 0.5f) { t2.text = ""; }
            if (p3.gameObject.GetComponent<Rigidbody>().velocity.x > 0.5f) { t3.text = ""; }
            if (p4.gameObject.GetComponent<Rigidbody>().velocity.x > 0.5f) { t4.text = ""; }
        }
        if (current == "go" && (alldone() || timer > 10))
        {
            current = "wheels";
            wheels.SetActive(true);
            set_text("wheels improve your traction.");
        }
        if (current == "wheels")
        {
            if (p1.m_TireLevel == PickupLevelEnum.two) { t1.text = ""; }
            if (p2.m_TireLevel == PickupLevelEnum.two) { t2.text = ""; }
            if (p3.m_TireLevel == PickupLevelEnum.two) { t3.text = ""; }
            if (p4.m_TireLevel == PickupLevelEnum.two) { t4.text = ""; }
        }
        if (current == "wheels" && (alldone() || timer > 40)) {
            current = "bodies";
            wheels.SetActive(false);
            bodies.SetActive(true);
            set_text("bodies improve your health.");
        }
        if (current == "bodies")
        {
            if (p1.m_CarBodyLevel == PickupLevelEnum.two) { t1.text = ""; }
            if (p2.m_CarBodyLevel == PickupLevelEnum.two) { t2.text = ""; }
            if (p3.m_CarBodyLevel == PickupLevelEnum.two) { t3.text = ""; }
            if (p4.m_CarBodyLevel == PickupLevelEnum.two) { t4.text = ""; }
        }
        if (current == "bodies" && (alldone() || timer > 60)) {
            current = "engines";
            bodies.SetActive(false);
            engines.SetActive(true);
            set_text("engines improve your speed.");
        }
        if (current == "engines")
        {
            if (p1.m_EngineLevel == PickupLevelEnum.two) { t1.text = ""; }
            if (p2.m_EngineLevel == PickupLevelEnum.two) { t2.text = ""; }
            if (p3.m_EngineLevel == PickupLevelEnum.two) { t3.text = ""; }
            if (p4.m_EngineLevel == PickupLevelEnum.two) { t4.text = ""; }
        }
        if (current == "engines" && (alldone() || timer > 80))
        {
            current = "rockets";
            engines.SetActive(false);
            rockets.SetActive(true);
            set_text("rockets bumpers: aim, b: shoot");
        }
        if (current == "rockets" && (alldone() || timer > 100))
        {
            current = "ctf";
            ctf.SetActive(true);
            set_text("pick up flags and drive through fire-rings to score");
            Left.Open();
            Right.Open();
        }
        if (current == "ctf" && (alldone() || timer > 120))
        {
            current = "";
            set_text("good luck!");
            StartCoroutine(GoodLuckText());
        }
    }

    private IEnumerator GoodLuckText()
    {
        yield return new WaitForSeconds(4);
        set_text("");
    }

    private void set_text(string t) {
        t1.text = t;
        t2.text = t;
        t3.text = t;
        t4.text = t;
    }

    private bool alldone() {
        if (current == "wheels" && p1.m_TireLevel == PickupLevelEnum.two && p2.m_TireLevel == PickupLevelEnum.two && p3.m_TireLevel == PickupLevelEnum.two && p4.m_TireLevel == PickupLevelEnum.two) {
            return true;
        }
        if (current == "bodies" && p1.m_CarBodyLevel == PickupLevelEnum.two && p2.m_CarBodyLevel == PickupLevelEnum.two && p3.m_CarBodyLevel == PickupLevelEnum.two && p4.m_CarBodyLevel == PickupLevelEnum.two)
        {
            return true;
        }
        if (current == "engines" && p1.m_EngineLevel == PickupLevelEnum.two && p2.m_EngineLevel == PickupLevelEnum.two && p3.m_EngineLevel == PickupLevelEnum.two && p4.m_EngineLevel == PickupLevelEnum.two)
        {
            return true;
        }
        else return false;
    }
}
