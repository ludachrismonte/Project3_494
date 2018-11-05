using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Start_Game : MonoBehaviour {
    public int count; 
	void Start () 
    {
        count = 0;
	}
	
	void Update () 
    {
        if (count >= 1800)
        {
            SceneManager.LoadScene("Arena");
        }
        count++;
	}
}
