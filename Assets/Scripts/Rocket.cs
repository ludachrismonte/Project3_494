using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour 
{
    public float rocketSpeed;
    public float objectLifeTimeValue = 100;
    public GameObject m_ExplosionPrefab;

    private float timerSinceLaunch;
    private Transform target;

    // Use this for initialization
    void Start()
    {
        timerSinceLaunch = 0;
        objectLifeTimeValue = 100;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(target);
        Debug.Log("my target is " + target.gameObject.tag);
        transform.position += transform.forward * rocketSpeed * Time.deltaTime;
        
        if (timerSinceLaunch > objectLifeTimeValue)
        {
            Destroy(transform.gameObject, 1);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (target != null && collision.gameObject.tag == target.gameObject.tag) 
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Health>().AlterHealth(-5f);
            Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
        }
    }

    public void SetTarget(GameObject t) {
        target = t.transform;
    }
}
