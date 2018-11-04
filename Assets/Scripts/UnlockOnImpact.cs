using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockOnImpact : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
