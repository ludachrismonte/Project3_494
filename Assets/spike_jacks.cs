using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike_jacks : MonoBehaviour
{

    //public GameObject
    /*int count;
    bool trapped;*/
    // Use this for initialization
    void Start()
    {
        /*count = 0;
        trapped = false;*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (trapped)
        {
            count++;
        }
        if (count >= 180 && trapped)
        {
            count = 0;
            trapped = false;
        }*/
    }

    /* private void OnCollisionEnter(Collision collision)
     {
         if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4")
         {
             //temporarily disable movement
             //trapped = true;
             collision.rigidbody.velocity = Vector3.zero;
             Destroy(gameObject);
         }
     }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Destroy(gameObject);
        }

    }
}
