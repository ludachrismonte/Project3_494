using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //public Image health_bar;
    public Image black;
    public GameObject cam;
    public GameObject[] m_CarBodyLevel;
    public GameObject m_ExplosionPrefab;
    public bool m_TestDeath = false;
    public Text m_PlayerMainText;

    private Vector3[] sizes = new Vector3[5];

    private float m_Health;
    private GameObject m_CurrentCarBody;
    private PlayerPickup m_PlayerPickup;
    private RespawnReset respawnReset;
    private bool invincible = false;
    private Score FlagMgr;
    private Rigidbody m_Rigidbody;

    private void Awake()
    {
        FlagMgr = GetComponent<Score>();
        m_PlayerPickup = GetComponent<PlayerPickup>();
        m_Rigidbody = GetComponent<Rigidbody>();
        respawnReset = GetComponent<RespawnReset>();

        sizes[0] = m_CarBodyLevel[0].transform.localScale;
        sizes[1] = m_CarBodyLevel[1].transform.localScale;
        sizes[2] = m_CarBodyLevel[2].transform.localScale;
        sizes[3] = m_CarBodyLevel[3].transform.localScale;
        sizes[4] = m_CarBodyLevel[4].transform.localScale;

        m_CurrentCarBody = m_CarBodyLevel[(int)m_PlayerPickup.m_CarBodyLevel - 1];
    }

    private void Update()
    {
        if (m_TestDeath)
        {
            SetHealth(0);
            m_TestDeath = false;
        }
        //health_bar.fillAmount = m_Health / 50;
    }

    public void AlterHealth(float amt)
    {
        GameObject shield = transform.Find("Shield").gameObject;
        if (shield.activeSelf) {
            shield.SetActive(false);
            return;
        }
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
        if (!invincible)
        {
            if (m_Health <= 0)
            {
                if (transform.position.y > 2.5)
                {
                    FlagMgr.DropFlag(false);
                }
                else
                {
                    FlagMgr.DropFlag(true);
                }
                gameObject.GetComponent<PlayerPickup>().Respawn();
                StartCoroutine(Die());
            }
            else if (m_Health <= 10)
            {
                StartCoroutine(Grow(0));
                m_PlayerPickup.m_CarBodyLevel = PickupLevelEnum.one;
            }
            else if (m_Health <= 20)
            {
                StartCoroutine(Grow(1));
                m_PlayerPickup.m_CarBodyLevel = PickupLevelEnum.two;
            }
            else if (m_Health <= 30)
            {
                StartCoroutine(Grow(2));
                m_PlayerPickup.m_CarBodyLevel = PickupLevelEnum.three;
            }
            else if (m_Health <= 40)
            {
                StartCoroutine(Grow(3));
                m_PlayerPickup.m_CarBodyLevel = PickupLevelEnum.four;
            }
            else
            {
                StartCoroutine(Grow(4));
                m_PlayerPickup.m_CarBodyLevel = PickupLevelEnum.five;
            }
        }
    }

    private IEnumerator Die()
    {
        invincible = true;
        Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
        Instantiate(m_ExplosionPrefab, transform.position + (transform.forward * 2), Quaternion.identity);
        Instantiate(m_ExplosionPrefab, transform.position - (transform.forward * 2), Quaternion.identity);
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.Find("SkyCar").gameObject.SetActive(false);

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, (float)(i / 1));
            yield return null;
        }
        respawnReset.Respawn(0);
        m_PlayerMainText.text = "knocked out!";
        yield return new WaitForSeconds(1f);

        for (float i = 1; i > 0; i -= Time.deltaTime)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, i);
            yield return null;
        }

        m_PlayerMainText.text = "";
        m_Rigidbody.constraints = RigidbodyConstraints.None;
        invincible = false;
    }

    private IEnumerator Grow(int level) {
        m_CarBodyLevel[level].SetActive(true);
        Vector3 initial = m_CurrentCarBody.transform.localScale;
        for (float i = 0f; i < 0.5f; i += Time.deltaTime)
        {
            m_CurrentCarBody.transform.localScale = initial * (1 - i);
            m_CarBodyLevel[level].transform.localScale = sizes[level] * (.5f + i);
            yield return null;
        }
        m_CurrentCarBody.transform.localScale = initial;
        m_CarBodyLevel[level].transform.localScale = sizes[level];
        m_CurrentCarBody.SetActive(false);
        m_CurrentCarBody = m_CarBodyLevel[level];
        m_CurrentCarBody.SetActive(true);
    }
}
