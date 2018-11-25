using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType { rocket, landmine, none };

public class WeaponManager : MonoBehaviour 
{
    public GameObject m_LeftRocket = null;
    public GameObject m_RightRocket = null;
    public GameObject m_RocketShooter;
    public GameObject m_RocketPrefab;
    public Text m_TargetingText = null;

    public GameObject m_CarLandmine;
    public GameObject m_LandminePrefab;

    private RocketTargeter m_RocketTargeter;
    private bool m_HasTwoRockets = false;
    private GameObject m_RocketTarget;
    private WeaponType m_CurrentWeapon = WeaponType.none;

    void Start () 
    {
        if (m_TargetingText == null)
        {
            Debug.LogError("ERROR in WeaponManager: TargetingText is null");
        }
        else
        {
            m_TargetingText.text = "";
        }

        m_RocketTargeter = GetComponent<RocketTargeter>();

        m_RightRocket.SetActive(false);
        m_LeftRocket.SetActive(false);
        m_RocketShooter.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Update()
    {
        if (m_CurrentWeapon == WeaponType.rocket) 
        {
            m_RocketTarget = m_RocketTargeter.GetTarget();
            if (m_RocketTarget != null)
            {
                m_RocketShooter.transform.LookAt(m_RocketTarget.transform);
                switch (m_RocketTarget.tag)
                {
                    case "Player":
                        m_TargetingText.text = "rocket lock: blue";
                        m_TargetingText.color = Color.blue;
                        break;
                    case "Player2":
                        m_TargetingText.text = "rocket lock: red";
                        m_TargetingText.color = Color.red;
                        break;
                    case "Player3":
                        m_TargetingText.text = "rocket lock: green";
                        m_TargetingText.color = Color.green;
                        break;
                    case "Player4":
                        m_TargetingText.text = "rocket lock: yellow";
                        m_TargetingText.color = Color.yellow;
                        break;
                    default:
                        Debug.LogError("ERROR in WeaponManager.cs: invalid target.");
                        break;
                }
            }
            else
            {
                m_TargetingText.text = "no rocket lock";
                m_TargetingText.color = Color.white;
            }
        }
    }

    public void EquipRocket() 
    {
        if (m_CurrentWeapon == WeaponType.none)
        {
            m_CurrentWeapon = WeaponType.rocket;
            StartCoroutine(RaiseRocketShooter());

            m_HasTwoRockets = true;
            m_LeftRocket.SetActive(true);
            m_RightRocket.SetActive(true);
        }
    }

    public void EquipLandmine()
    {
        if (m_CurrentWeapon == WeaponType.none) 
        {
            m_CurrentWeapon = WeaponType.landmine;
            m_CarLandmine.SetActive(true);
        }
    }

    public void Fire() 
    {
        if (m_CurrentWeapon == WeaponType.rocket && m_RocketTarget != null)
        {
            if (m_HasTwoRockets)
            {
                GameObject rocket = Instantiate(m_RocketPrefab, 
                                                m_LeftRocket.transform.position, 
                                                m_LeftRocket.transform.rotation
                                               ) as GameObject;
                rocket.GetComponent<Rocket>().SetTarget(m_RocketTarget);
                m_HasTwoRockets = false;
                m_LeftRocket.SetActive(false);
            }
            else
            {
                GameObject rocket = Instantiate(m_RocketPrefab,
                                                m_RightRocket.transform.position,
                                                m_RightRocket.transform.rotation
                                               ) as GameObject;
                rocket.GetComponent<Rocket>().SetTarget(m_RocketTarget);
                m_RightRocket.SetActive(false);
                StartCoroutine(LowerRocketShooter());
                m_CurrentWeapon = WeaponType.none;
            }
        }
        else if (m_CurrentWeapon == WeaponType.landmine)
        {
            m_CarLandmine.SetActive(true);
            Instantiate(m_LandminePrefab, 
                        m_CarLandmine.transform.position, 
                        m_CarLandmine.transform.rotation);
            m_CurrentWeapon = WeaponType.none;
        }
    }

    private IEnumerator RaiseRocketShooter() 
    {
        m_RocketShooter.GetComponent<MeshRenderer>().enabled = true;
        for (float i = .6f; i < 1.5f; i += .05f) 
        {
            m_RocketShooter.transform.localPosition += new Vector3(0, .05f, 0);
            yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator LowerRocketShooter()
    {
        yield return new WaitForSeconds(.3f);
        for (float i = .6f; i < 1.5f; i += .05f)
        {
            m_RocketShooter.transform.localPosition -= new Vector3(0, .05f, 0);
            yield return new WaitForSeconds(0);
        }
        m_RocketShooter.GetComponent<MeshRenderer>().enabled = false;
    }

    public IEnumerator TutorialRocket()
    {
        StartCoroutine(RaiseRocketShooter());
        yield return new WaitForSeconds(4);
        StartCoroutine(LowerRocketShooter());
    }

    public IEnumerator TutorialLandmine()
    {
        m_CarLandmine.SetActive(true);
        yield return new WaitForSeconds(4);
        m_CarLandmine.SetActive(false);
    }

    public WeaponType GetCurrentWeapon()
    {
        return m_CurrentWeapon;
    }

}
