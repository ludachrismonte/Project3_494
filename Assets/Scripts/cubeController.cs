using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class cubeController : MonoBehaviour {
    Rigidbody rb;
    public float speed;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal") ;
        float v = Input.GetAxis("Vertical") ;
        //Debug.Log(v);
        rb.velocity = new Vector3(h * speed, rb.velocity.y, v * speed);
	}
}
