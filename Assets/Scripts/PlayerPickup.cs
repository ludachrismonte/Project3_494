using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class PlayerPickup : MonoBehaviour
{
    public float m_SecondsToRespawnPickup = 60f;
    public PickupLevelEnum m_CarBodyLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_TireLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_EngineLevel = PickupLevelEnum.one;
    public GameObject UIObjects;

    private CarController m_CarController;
    private Health m_CarHealth;

    private RawImage spedometer_1;
    private RawImage spedometer_2;
    private RawImage spedometer_3;
    private RawImage spedometer_4;
    private RawImage spedometer_5;

    private void Start()
    {
        m_CarController = GetComponent<CarController>();
        m_CarHealth = GetComponent<Health>();

        spedometer_1 = UIObjects.transform.Find("spedometer_1").gameObject.GetComponent<RawImage>();
        spedometer_2 = UIObjects.transform.Find("spedometer_2").gameObject.GetComponent<RawImage>();
        spedometer_3 = UIObjects.transform.Find("spedometer_3").gameObject.GetComponent<RawImage>();
        spedometer_4 = UIObjects.transform.Find("spedometer_4").gameObject.GetComponent<RawImage>();
        spedometer_5 = UIObjects.transform.Find("spedometer_5").gameObject.GetComponent<RawImage>();

        UpdateCarBody();
        UpdateEngine();
        UpdateTires();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarBodyPickup")
        {
            PickupLevel pickupLevel = other.GetComponent<PickupLevel>();
            if (pickupLevel.m_PickupLevel > m_CarBodyLevel)
            {
                m_CarBodyLevel = pickupLevel.m_PickupLevel;
                UpdateCarBody();
            }
            StartCoroutine(WaitToRespawn(other.gameObject));
        }

        if (other.tag == "EnginePickup")
        {
            PickupLevel pickupLevel = other.GetComponent<PickupLevel>();
            if (pickupLevel.m_PickupLevel > m_EngineLevel)
            {
                m_EngineLevel = pickupLevel.m_PickupLevel;
                UpdateEngine();
            }
            StartCoroutine(WaitToRespawn(other.gameObject));
        }

        if (other.tag == "TirePickup")
        {
            PickupLevel pickupLevel = other.GetComponent<PickupLevel>();
            if (pickupLevel.m_PickupLevel > m_TireLevel)
            {
                m_TireLevel = pickupLevel.m_PickupLevel;
                UpdateTires();
            }
            StartCoroutine(WaitToRespawn(other.gameObject));
        }

        if (other.tag == "RocketPickup")
        {
            GetComponent<WeaponManager>().get_rocket();
            Destroy(other.transform.parent.transform.parent.gameObject);
            RocketDrops rocketDrops = GameObject.Find("Manager").GetComponent<RocketDrops>();
            rocketDrops.RocketPickedUp();
        }

        if (other.tag == "ShieldPickup")
        {
            transform.Find("Shield").gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(WaitToRespawn(other.gameObject));
        }

        if (other.tag == "LandminePickup")
        {
            GetComponent<WeaponManager>().get_landmine();
            Destroy(other.gameObject);
        }
    }

    private void UpdateCarBody()
    {
        switch (m_CarBodyLevel)
        {
            case PickupLevelEnum.two:
                m_CarHealth.SetHealth(20);
                break;
            case PickupLevelEnum.three:
                m_CarHealth.SetHealth(30);
                break;
            case PickupLevelEnum.four:
                m_CarHealth.SetHealth(40);
                break;
            case PickupLevelEnum.five:
                m_CarHealth.SetHealth(50);
                break;
            default:
                m_CarHealth.SetHealth(10);
                break;
        }
    }

    private void UpdateEngine()
    {
        spedometer_1.enabled = false;
        spedometer_2.enabled = false;
        spedometer_3.enabled = false;
        spedometer_4.enabled = false;
        spedometer_5.enabled = false;

        switch (m_EngineLevel)
        {
            case PickupLevelEnum.two:
                m_CarController.MaxSpeed = 75;
                spedometer_2.enabled = true;
                break;
            case PickupLevelEnum.three:
                m_CarController.MaxSpeed = 100;
                spedometer_3.enabled = true;
                break;
            case PickupLevelEnum.four:
                m_CarController.MaxSpeed = 125;
                spedometer_4.enabled = true;
                break;
            case PickupLevelEnum.five:
                m_CarController.MaxSpeed = 150;
                spedometer_5.enabled = true;
                break;
            default:
                m_CarController.MaxSpeed = 50;
                spedometer_1.enabled = true;
                break;
        }
    }

    private void UpdateTires()
    {
        switch (m_TireLevel)
        {
            case PickupLevelEnum.two:
                m_CarController.SteerHelperValue = 0.7f;
                break;
            case PickupLevelEnum.three:
                m_CarController.SteerHelperValue = 0.8f;
                break;
            case PickupLevelEnum.four:
                m_CarController.SteerHelperValue = 0.9f;
                break;
            case PickupLevelEnum.five:
                m_CarController.SteerHelperValue = 1.0f;
                break;
            default:
                m_CarController.SteerHelperValue = 0.6f;
                break;
        }
    }

    private IEnumerator WaitToRespawn(GameObject pickup)
    {
        pickup.SetActive(false);
        yield return new WaitForSeconds(m_SecondsToRespawnPickup);
        pickup.SetActive(true);
    }

    public void Respawn()
    {
        GameObject shield = transform.Find("Shield").gameObject;
        shield.SetActive(false);

        m_CarBodyLevel = PickupLevelEnum.one;
        UpdateCarBody();
        m_TireLevel = PickupLevelEnum.one;
        UpdateTires();
        m_EngineLevel = PickupLevelEnum.one;
        UpdateEngine();
    }
}