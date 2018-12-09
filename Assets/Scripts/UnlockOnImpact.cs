using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockOnImpact : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None;
    }
}
