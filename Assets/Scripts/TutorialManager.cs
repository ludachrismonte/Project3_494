using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public GameObject[] Scenes;
    public float scene_rate;
    
	// Use this for initialization
	void Start () {
        for (int i = 0; i < Scenes.Length; i++) {
            Scenes[i].SetActive(false);
        }
        StartCoroutine(Play());
    }

    IEnumerator Play() {
        for (int i = 0; i < Scenes.Length; i++)
        {
            Scenes[i].SetActive(true);
            yield return new WaitForSeconds(scene_rate);
            Scenes[i].SetActive(false);
        }
        SceneManager.LoadScene("Arena");
    }
}
