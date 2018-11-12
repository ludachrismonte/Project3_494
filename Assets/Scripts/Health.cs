using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    
    //public float damageMultrest;
    //public float damageMultfront;
    public Image health_bar;
    public GameObject cam;
    public GameObject[] m_CarBodyLevel;

    private float m_Health;
    //private Rigidbody carRb;
    //private Respawn respawn;
    //private Vector3 initialVel = Vector3.zero;
    //private Vector3 finalVel = Vector3.zero;
    private GameObject m_CurrentCarBody;

    private void Start () 
    {
        m_CurrentCarBody = m_CarBodyLevel[0];
    }
    
    private void Update () 
    {
        health_bar.fillAmount = m_Health / 50;
    }

    public void AlterHealth(float amt)
    {
        m_Health += amt;
        UpdateCarBody();
    }

    public void SetHealth(float amt)
    {
        m_Health = amt;
        UpdateCarBody();
    }

    public float GetHealth()
    {
        return m_Health;
    }

    private void UpdateCarBody()
    {
        if (m_Health <= 0)
        {
            if (transform.position.y > 2.5)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            gameObject.GetComponent<PlayerPickup>().Respawn();
            StartCoroutine(GameObject.FindWithTag("GameManager").GetComponent<DeathMaster>().Die(gameObject));
        }
        else if (m_Health <= 10)
        {
            m_CurrentCarBody.SetActive(false);
            m_CarBodyLevel[0].SetActive(true);
            m_CurrentCarBody = m_CarBodyLevel[0];
        }
        else if (m_Health <= 20)
        {
            m_CurrentCarBody.SetActive(false);
            m_CarBodyLevel[1].SetActive(true);
            m_CurrentCarBody = m_CarBodyLevel[1];
        }
        else if (m_Health <= 30)
        {
            m_CurrentCarBody.SetActive(false);
            m_CarBodyLevel[2].SetActive(true);
            m_CurrentCarBody = m_CarBodyLevel[2];
        }
        else if (m_Health <= 40)
        {
            m_CurrentCarBody.SetActive(false);
            m_CarBodyLevel[3].SetActive(true);
            m_CurrentCarBody = m_CarBodyLevel[3];
        }
        else
        {
            m_CurrentCarBody.SetActive(false);
            m_CarBodyLevel[4].SetActive(true);
            m_CurrentCarBody = m_CarBodyLevel[4];
        }
    }

    //Make sure the empty game objects can't collide with the car

    //private void OnCollisionEnter(Collision collision)
    //{
    //    initialVel = carRb.velocity;
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.forward, out hit))
    //    {
    //        if(hit.collider == collision.collider)
    //        {
    //            Debug.Log("Point of contact is front");
    //            damageMult = damageMultfront;
    //        }
    //        else
    //        {
    //            Debug.Log("Point of contact is not front");
    //            damageMult = damageMultrest;
    //        }

    //    }
    //}

    //IEnumerator damageEnum()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    finalVel = carRb.velocity;
    //    health -= Mathf.Abs(finalVel.magnitude - initialVel.magnitude) * damageMult;
    //    initialVel = Vector3.zero;
    //}
}
