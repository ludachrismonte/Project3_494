using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePulse : MonoBehaviour {

    private Vector3 initial;
    private float multiplier;
    private float timer;
    public float speed = 6f;

	// Use this for initialization
	void Start () {
        timer = 0f;
        initial = gameObject.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime * speed;
        multiplier = Mathf.Sin(timer);
        multiplier = (multiplier / 5) + 1;
        gameObject.transform.localScale = initial * multiplier;
    }
}
