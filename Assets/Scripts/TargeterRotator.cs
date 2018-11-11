using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargeterRotator : MonoBehaviour {

    public float speed = 20f;
    void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
