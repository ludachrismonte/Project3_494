using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour {

    private Transform target;

    public void Start()
    {
        target = GameObject.FindWithTag("ScoreZone").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(target);
    }
}

