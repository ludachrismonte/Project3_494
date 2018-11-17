using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{
    public Image health_bar;
    public GameObject cam;
    public GameObject[] m_CarBodyLevel;
    public GameObject m_ExplosionPrefab;

    private float m_Health;
    private GameObject m_CurrentCarBody;
    private PlayerPickup m_PlayerPickup;

    private void Start () 
    {
        m_PlayerPickup = GetComponent<PlayerPickup>();
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
            Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
            Instantiate(m_ExplosionPrefab, transform.position + (transform.forward * 2), Quaternion.identity);
            Instantiate(m_ExplosionPrefab, transform.position - (transform.forward * 2), Quaternion.identity);
            StartCoroutine(Die(gameObject));
        }
        else if (m_Health <= 10)
        {
            m_CurrentCarBody.SetActive(false);
            m_CarBodyLevel[0].SetActive(true);
            m_CurrentCarBody = m_CarBodyLevel[0];
            m_PlayerPickup.m_CarBodyLevel = PickupLevelEnum.one;
        }
        else if (m_Health <= 20)
        {
            m_CurrentCarBody.SetActive(false);
            m_CarBodyLevel[1].SetActive(true);
            m_CurrentCarBody = m_CarBodyLevel[1];
            m_PlayerPickup.m_CarBodyLevel = PickupLevelEnum.two;
        }
        else if (m_Health <= 30)
        {
            m_CurrentCarBody.SetActive(false);
            m_CarBodyLevel[2].SetActive(true);
            m_CurrentCarBody = m_CarBodyLevel[2];
            m_PlayerPickup.m_CarBodyLevel = PickupLevelEnum.three;
        }
        else if (m_Health <= 40)
        {
            m_CurrentCarBody.SetActive(false);
            m_CarBodyLevel[3].SetActive(true);
            m_CurrentCarBody = m_CarBodyLevel[3];
            m_PlayerPickup.m_CarBodyLevel = PickupLevelEnum.four;

        }
        else
        {
            m_CurrentCarBody.SetActive(false);
            m_CarBodyLevel[4].SetActive(true);
            m_CurrentCarBody = m_CarBodyLevel[4];
            m_PlayerPickup.m_CarBodyLevel = PickupLevelEnum.five;

        }
    }

    private IEnumerator Die(GameObject player)
    {
        player.GetComponent<ControllerInput>().enabled = false;
        GameObject car = player.transform.Find("SkyCar").gameObject;
        car.SetActive(false);
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 4; i++)
        {
            car.SetActive(true);
            yield return new WaitForSeconds(.15f);
            car.SetActive(false);
            yield return new WaitForSeconds(.15f);
        }
        car.SetActive(true);
        player.GetComponent<ControllerInput>().enabled = true;
    }
}
