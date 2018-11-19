using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    private float timer;
    public int reverse = 1;

    void Start () {
        timer = 0;
	}
	
	void Update () {
        timer += Time.deltaTime;
        if (timer < 4) {
            transform.Rotate(reverse * Vector3.forward, 30f * Time.deltaTime);
        }
    }
}
