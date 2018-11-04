using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyUpManager : MonoBehaviour {

    public static int numReadied = 0;
    public string nextScene;
	
	// Update is called once per frame
	void Update () {
		
        // Check for scene change
        if (numReadied >= 4){

            // Change scenes
            SceneManager.LoadScene(nextScene);

        }

	}
}
