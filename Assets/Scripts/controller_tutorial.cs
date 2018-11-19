using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class controller_tutorial : MonoBehaviour {
    public Image Abutton;
    //public Image Bbutton;
    //public Image Xbutton;
    //public Image Ybutton;
    //public Image tire;
    //public Image body;
    //public Image engine;
    //public Image rocket;
    private Vector3 newposition;
    public bool ready;
    public int count;
    private bool startCount;


	// Use this for initialization
	void Start () {
        //Abutton = GetComponent<Image>();
        //Bbutton = GetComponent<Image>();
        //Xbutton = GetComponent<Image>();
        //Ybutton = GetComponent<Image>();
        /*tire = GetComponent<Image>();
        body = GetComponent<Image>();
        engine = GetComponent<Image>();*/
        Abutton.enabled = true;
        //Abutton.enabled = true;
        /*tire.enabled = false;
        body.enabled = false;
        engine.enabled = false;
        rocket.enabled = false;*/
    }

    // Update is called once per frame
    void Update() {
        if (startCount == true)
        {
            count++;
        }
        if (count >= 300 || ready)
        {
            startCount = false;
            Abutton.enabled = false;
            /*else if (tire.enabled)
            {
                tire.enabled = false;
            }
            else if (body.enabled)
            {
                body.enabled = false;
            }
            else if (engine.enabled)
            {
                engine.enabled = false;
            }
            else if (rocket.enabled)
            {
                rocket.enabled = false;
            }*/
        }
        if (ready)
        {
            return;
        }
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CarBodyPickup")
        {
            tire.enabled = false;
            body.enabled = true;
            engine.enabled = false;
            rocket.enabled = false;
            startCount = true;
            count = 0;
        }
        if (collision.gameObject.tag == "EnginePickup")
        {
            tire.enabled = false;
            body.enabled = false;
            engine.enabled = true;
            rocket.enabled = false;
            startCount = true;
            count = 0;
        }
        if (collision.gameObject.tag == "TirePickup")
        {
            tire.enabled = true;
            body.enabled = false;
            engine.enabled = false;
            rocket.enabled = false;
            startCount = true;
            count = 0;
        }
        if (collision.gameObject.tag == "RocketPickup")
        {
            tire.enabled = false;
            body.enabled = false;
            engine.enabled = false;
            rocket.enabled = true;
            startCount = true;
            count = 0;
            //add rocket button
        }
    }*/
}
