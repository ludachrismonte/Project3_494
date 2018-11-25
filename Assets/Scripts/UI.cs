using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject[] speedometer;
    Rigidbody[] carRb;

    void Start()
    {
        carRb = new Rigidbody[4];
        carRb[0] = GameObject.FindWithTag("Player").gameObject.GetComponent<Rigidbody>();
        carRb[1] = GameObject.FindWithTag("Player2").gameObject.GetComponent<Rigidbody>();
        carRb[2] = GameObject.FindWithTag("Player3").gameObject.GetComponent<Rigidbody>();
        carRb[3] = GameObject.FindWithTag("Player4").gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        //SpeedoMeter
        //Player1
        float speed = carRb[0].velocity.magnitude * 3.6f;
        speedometer[0].transform.eulerAngles = new Vector3(0, 0, 515 - speed);
        //Player2
        speed = carRb[1].velocity.magnitude * 3.6f;
        speedometer[1].transform.eulerAngles = new Vector3(0, 0, 515 - speed);
        //Player3
        speed = carRb[2].velocity.magnitude * 3.6f;
        speedometer[2].transform.eulerAngles = new Vector3(0, 0, 515 - speed);
        //Player4
        speed = carRb[3].velocity.magnitude * 3.6f;
        speedometer[3].transform.eulerAngles = new Vector3(0, 0, 515 - speed);
    }
}