﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scale_pulse : MonoBehaviour {

    private Vector3 initial;
    private float multiplier;
    private float timer;

	// Use this for initialization
	void Start () {
        timer = 0f;
        initial = gameObject.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        multiplier = Mathf.Sin(timer);
        multiplier = (multiplier / 5) + 1;
        gameObject.transform.localScale = initial * multiplier;
    }
}
