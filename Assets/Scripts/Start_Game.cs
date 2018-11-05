using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Start_Game : MonoBehaviour {
    public int count; 
	// Use this for initialization
	void Start () {
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (count >= 1800)
        {
            SceneManager.LoadScene("Arena");
        }
        count++;
	}
}
