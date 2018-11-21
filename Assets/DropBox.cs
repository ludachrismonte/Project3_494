using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour {

    private GameObject parachute;
    private GameObject closed_crate;
    private GameObject open_crate;

	// Use this for initialization
	void Start () {
        parachute = transform.Find("parachute").gameObject;
        closed_crate = transform.Find("closed_crate").gameObject;
        open_crate = transform.Find("open_crate").gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        closed_crate.SetActive(false);
        parachute.SetActive(false);
        open_crate.SetActive(true);
    }
}
