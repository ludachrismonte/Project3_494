using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class PlayerPickup : MonoBehaviour
{
    public GameObject m_CarBodyObject;
    public float m_SecondsToRespawnPickup = 60f;
    public PickupLevelEnum m_CarBodyLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_TireLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_EngineLevel = PickupLevelEnum.one;

    private CarController m_CarController;
    private Health m_CarHealth;

    private void Start()
    {
        m_CarController = GetComponent<CarController>();
        m_CarHealth = GetComponent<Health>();

        UpdateCarBody();
        UpdateEngine();
        UpdateTires();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarBodyPickup")
        {
            print("hit");
            m_CarBodyObject.SetActive(true);

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

        if (other.tag == "ScoreZone")
        {
            other.gameObject.GetComponent<RandomPlacement>().Move();
        }
    }

    private void UpdateCarBody()
    {
        switch (m_CarBodyLevel)
        {
            case PickupLevelEnum.two:
                m_CarHealth.health = 20;
                break;
            case PickupLevelEnum.three:
                m_CarHealth.health = 30;
                break;
            case PickupLevelEnum.four:
                m_CarHealth.health = 40;
                break;
            case PickupLevelEnum.five:
                m_CarHealth.health = 50;
                break;
            default:
                m_CarHealth.health = 10;
                break;
        }
    }

    private void UpdateEngine()
    {
        switch (m_EngineLevel)
        {
            case PickupLevelEnum.two:
                m_CarController.MaxSpeed = 75;
                break;
            case PickupLevelEnum.three:
                m_CarController.MaxSpeed = 75;
                break;
            case PickupLevelEnum.four:
                m_CarController.MaxSpeed = 100;
                break;
            case PickupLevelEnum.five:
                m_CarController.MaxSpeed = 150;
                break;
            default:
                m_CarController.MaxSpeed = 50;
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
}