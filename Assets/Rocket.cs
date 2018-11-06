using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    private GameObject target;
    private Transform rootNode;

    // Use this for initialization
    void Start () {
        target = null;
        rootNode = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update () {
        if (target != null) {
            transform.LookAt(target.transform.position);
            transform.Translate(Vector3.forward * 30 * Time.deltaTime);
        }
	}

    public void fire(GameObject t) {
        target = t;
        rootNode.parent = null;
    }
}
