using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour 
{
    public float rocketSpeed;
    public float objectLifeTimeValue = 100;
    public GameObject m_ExplosionPrefab;
    public AudioClip explode;
    public float m_RocketDamage = 10f;

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
            AudioSource.PlayClipAtPoint(explode, transform.position);
            Destroy(gameObject);
            collision.gameObject.GetComponent<Health>().AlterHealth(m_RocketDamage);
            Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
        }
    }

    public void SetTarget(GameObject t) {
        target = t.transform;
    }
}
