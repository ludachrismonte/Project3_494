using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour 
{
    public float m_RocketSpeed = 100f;
    public float m_RocketLifetime = 15f;
    public GameObject m_ExplosionPrefab;
    public AudioClip m_ExplosionAudio;
    public float m_RocketDamage = 5f;

    private float timerSinceLaunch = 0;
    private Transform target;

    void LateUpdate()
    {
        transform.LookAt(target);
        timerSinceLaunch += Time.deltaTime;

        transform.position += transform.forward * m_RocketSpeed * Time.deltaTime;
        
        if (timerSinceLaunch > m_RocketLifetime)
        {
            Destroy(transform.gameObject, 1);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (target != null && collision.gameObject.tag == target.gameObject.tag) 
        {
            AudioSource.PlayClipAtPoint(m_ExplosionAudio, transform.position);
            Destroy(gameObject);
            Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
            GameObject shield = target.gameObject.transform.Find("Shield").gameObject;

            if (shield.activeSelf)
                shield.SetActive(false);
            else
                collision.gameObject.GetComponent<Health>().AlterHealth(-m_RocketDamage);
        }
    }

    public void SetTarget(GameObject t) 
    {
        target = t.transform;
    }
}
