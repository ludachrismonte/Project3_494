using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class PlayerPickup : MonoBehaviour 
{
    public GameObject carBodyObject;
    public PickupLevelEnum m_CarBodyLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_EngineLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_TireLevel = PickupLevelEnum.one;

    private Health m_CarHealth;
    private CarController m_CarController;

    private void Start()
    {
        m_CarHealth = GetComponent<Health>();
        m_CarController = GetComponent<CarController>();

        UpdateCarBody();
        UpdateEngine();
        UpdateTires();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarBodyPickup")
        {
            carBodyObject.SetActive(true);
            Destroy(other.gameObject);
            UpdateCarBody();
        }

        if (other.tag == "EnginePickup")
        {
            Destroy(other.gameObject);
            UpdateEngine();
        }

        if (other.tag == "TirePickup")
        {
            Destroy(other.gameObject);
            UpdateTires();
        }

        if (other.tag == "ScoreZone")
        {
            GameObject zone = GameObject.FindWithTag("ScoreZone");
            if (zone == null) {
                Debug.Log("didnt find it");
            }
            zone.GetComponent<RandomPlacement>().Move();
        }
    }

    public void UpdateCarBody()
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
}
