using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetUI : MonoBehaviour
{

    public GameObject [] resetText;
    //public RawImage [] resetImage;
    RespawnReset[] respawnResets;
    bool start = false;
    // Use this for initialization
    void Start()
    {
        respawnResets = new RespawnReset[4];
        respawnResets[0] = GameObject.FindWithTag("Player").gameObject.GetComponent<RespawnReset>();
        respawnResets[1] = GameObject.FindWithTag("Player2").gameObject.GetComponent<RespawnReset>();
        respawnResets[2] = GameObject.FindWithTag("Player3").gameObject.GetComponent<RespawnReset>();
        respawnResets[3] = GameObject.FindWithTag("Player4").gameObject.GetComponent<RespawnReset>();
    }
    // Update is called once per frame
    void Update()
    {
        //Respawn
        if(start){
            //Player1
            if (respawnResets[0].stuck) { resetText[0].GetComponent<Text>().enabled = true; }
            else { resetText[0].GetComponent<Text>().enabled = false; }
            //Player2
            if (respawnResets[1].stuck) { resetText[1].GetComponent<Text>().enabled = true; }
            else { resetText[1].GetComponent<Text>().enabled = false; }
            //Player3
            if (respawnResets[2].stuck) { resetText[2].GetComponent<Text>().enabled = true; }
            else { resetText[2].GetComponent<Text>().enabled = false; }
            //Player4
            if (respawnResets[3].stuck) { resetText[3].GetComponent<Text>().enabled = true; }
            else { resetText[3].GetComponent<Text>().enabled = false; } 
        }
    }

    IEnumerator startDelay()
    {
        yield return new WaitForSeconds(10f);
        //SceneManager.LoadScene("MainMenu");
        start = true;
    }

}