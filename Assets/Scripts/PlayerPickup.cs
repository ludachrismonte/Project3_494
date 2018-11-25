using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class PlayerPickup : MonoBehaviour
{
    public float m_SecondsToRespawnPickup = 30f;
    public PickupLevelEnum m_CarBodyLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_TireLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_EngineLevel = PickupLevelEnum.one;
    public RawImage[] m_Speedometers;

    private CarController m_CarController;
    private Health m_CarHealth;
    private Rigidbody m_Rigidbody;
    private WeaponManager m_WeaponManager;

    private RawImage m_CurrentSpeedometer;

    private void Start()
    {
        m_CarController = GetComponent<CarController>();
        m_CarHealth = GetComponent<Health>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_WeaponManager = GetComponent<WeaponManager>();

        foreach (RawImage speedometer in m_Speedometers)
            speedometer.enabled = false;

        m_CurrentSpeedometer = m_Speedometers[0];
        m_CurrentSpeedometer.enabled = true;

        UpdateCarBody();
        UpdateEngine();
        UpdateTires();
    }

    private void OnTriggerEnter(Collider other)
    {
        PickupLevel pickupLevel;
        switch (other.tag)
        {
            case "CarBodyPickup":
                pickupLevel = other.GetComponent<PickupLevel>();
                if (pickupLevel.m_PickupLevel > m_CarBodyLevel)
                {
                    m_CarBodyLevel = pickupLevel.m_PickupLevel;
                    UpdateCarBody();
                }
                StartCoroutine(WaitToRespawn(other.gameObject));
                break;
            case "EnginePickup":
                pickupLevel = other.GetComponent<PickupLevel>();
                if (pickupLevel.m_PickupLevel > m_EngineLevel)
                {
                    m_EngineLevel = pickupLevel.m_PickupLevel;
                    UpdateEngine();
                }
                StartCoroutine(WaitToRespawn(other.gameObject));
                break;
            case "TirePickup":
                pickupLevel = other.GetComponent<PickupLevel>();
                if (pickupLevel.m_PickupLevel > m_TireLevel)
                {
                    m_TireLevel = pickupLevel.m_PickupLevel;
                    UpdateTires();
                }
                StartCoroutine(WaitToRespawn(other.gameObject));
                break;
            case "RocketPickup":
                m_WeaponManager.get_rocket();
                Destroy(other.transform.parent.transform.parent.gameObject);
                RocketDrops rocketDrops = GameObject.Find("Manager").GetComponent<RocketDrops>();
                rocketDrops.RocketPickedUp();
                break;
            case "ShieldPickup":
                transform.Find("Shield").gameObject.SetActive(true);
                Destroy(other.gameObject);
                break;
            case "LandminePickup":
                m_WeaponManager.get_landmine();
                Destroy(other.gameObject);
                break;
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
        m_CurrentSpeedometer.enabled = false;

        switch (m_EngineLevel)
        {
            case PickupLevelEnum.two:
                m_CarController.MaxSpeed = 75;
                m_CurrentSpeedometer = m_Speedometers[1];
                break;
            case PickupLevelEnum.three:
                m_CarController.MaxSpeed = 100;
                m_CurrentSpeedometer = m_Speedometers[2];
                break;
            case PickupLevelEnum.four:
                m_CarController.MaxSpeed = 125;
                m_CurrentSpeedometer = m_Speedometers[3];
                break;
            case PickupLevelEnum.five:
                m_CurrentSpeedometer = m_Speedometers[4];
                m_CarController.MaxSpeed = 150;
                break;
            default:
                m_CarController.MaxSpeed = 50;
                m_CurrentSpeedometer = m_Speedometers[0];
                break;
        }

        m_CurrentSpeedometer.enabled = true;
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