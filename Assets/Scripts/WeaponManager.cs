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
    public GameObject m_Laser;

    public GameObject m_CarLandmine;
    public GameObject m_LandminePrefab;

    private AudioSource m_MechanicalNoise;
    private RocketTargeter m_RocketTargeter;
    private bool m_HasTwoRockets = false;
    private GameObject m_RocketTarget;
    private WeaponType m_CurrentWeapon = WeaponType.none;

    void Start () 
    {
        m_MechanicalNoise = GetComponent<AudioSource>();
        m_RocketTargeter = GetComponent<RocketTargeter>();

        m_RightRocket.SetActive(false);
        m_LeftRocket.SetActive(false);
        m_Laser.SetActive(false);
        m_Laser.gameObject.GetComponent<LineRenderer>().startWidth = 0.5f;
        m_RocketShooter.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Update()
    {
        if (m_CurrentWeapon == WeaponType.rocket) 
        {
            m_RocketTarget = m_RocketTargeter.GetTarget();
            if (m_RocketTarget != null)
            {
                m_Laser.SetActive(true);
                m_RocketShooter.transform.LookAt(m_RocketTarget.transform);
                return;
            }
        }
        m_RocketShooter.transform.localRotation = Quaternion.identity;
        m_Laser.SetActive(false);
    }

    public void EquipRocket() 
    {
        if (m_CurrentWeapon == WeaponType.none)
        {
            m_CurrentWeapon = WeaponType.rocket;
            StartCoroutine(RaiseRocketShooter());
        }
    }

    public void UnEquipRockets()
    {
        if (m_CurrentWeapon == WeaponType.rocket)
        {
            m_CurrentWeapon = WeaponType.none;
            StartCoroutine(LowerRocketShooter());
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

    public void UnequipLandmine()
    {
        if (m_CurrentWeapon == WeaponType.landmine)
        {
            m_CurrentWeapon = WeaponType.none;
            m_CarLandmine.SetActive(false);
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
            UnequipLandmine();
            Instantiate(m_LandminePrefab, 
                        m_CarLandmine.transform.position, 
                        m_CarLandmine.transform.rotation);
        }
    }

    private IEnumerator RaiseRocketShooter() 
    {
        m_MechanicalNoise.Play();
        m_HasTwoRockets = true;
        m_LeftRocket.SetActive(true);
        m_RightRocket.SetActive(true);

        m_RocketShooter.GetComponent<MeshRenderer>().enabled = true;
        for (float i = .6f; i < 1.5f; i += .05f) 
        {
            m_RocketShooter.transform.localPosition += new Vector3(0, .05f, 0);
            yield return new WaitForSeconds(0);
        }
        m_MechanicalNoise.Stop();
    }

    private IEnumerator LowerRocketShooter()
    {
        m_MechanicalNoise.Play();
        m_HasTwoRockets = false;
        m_LeftRocket.SetActive(false);
        m_RightRocket.SetActive(false);

        yield return new WaitForSeconds(.3f);
        for (float i = .6f; i < 1.5f; i += .05f)
        {
            m_RocketShooter.transform.localPosition -= new Vector3(0, .05f, 0);
            yield return new WaitForSeconds(0);
        }
        m_MechanicalNoise.Stop();
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
