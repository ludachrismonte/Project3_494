using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    private Transform target;
    public float turnplier;
    public float rocketSpeed;

    private float timerSinceLaunch;
    private float objectLifeTimerValue;

    // Use this for initialization
    void Start()
    {
        rocketSpeed = 20f;

        timerSinceLaunch = 0;
        objectLifeTimerValue = 100;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(target);
        transform.position += transform.forward * rocketSpeed * Time.deltaTime;
        
        if (timerSinceLaunch > objectLifeTimerValue)
        {
            Destroy(transform.gameObject, 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == target.tag) {
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject t) {
        target = t.transform;
    }
}
