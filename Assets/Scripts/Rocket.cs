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
        timerSinceLaunch = 0;
        objectLifeTimerValue = 100;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(target);
        Debug.Log("my target is " + target.gameObject.tag);
        transform.position += transform.forward * rocketSpeed * Time.deltaTime;
        
        if (timerSinceLaunch > objectLifeTimerValue)
        {
            Destroy(transform.gameObject, 1);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (target != null && collision.gameObject.tag == target.gameObject.tag) {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Health>().take_damage(5f);
        }
    }

    public void SetTarget(GameObject t) {
        target = t.transform;
    }
}
