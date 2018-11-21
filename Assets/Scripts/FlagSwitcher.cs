using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSwitcher : MonoBehaviour {

    public GameObject[] flags;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < flags.Length; i++) {
            flags[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
