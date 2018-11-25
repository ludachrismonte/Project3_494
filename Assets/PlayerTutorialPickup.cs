using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class PlayerTutorialPickup : MonoBehaviour
{
    public PickupLevelEnum m_CarBodyLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_TireLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_EngineLevel = PickupLevelEnum.one;
    public Text m_PlayerMainText;
    public RawImage[] m_Speedometers;
    public GameObject m_TutorialDoor;

    private CarController m_CarController;
    private Health m_CarHealth;
    private Rigidbody m_Rigidbody;
    private RawImage m_CurrentSpeedometer;
    
    private void Start()
    {
        m_CarController = GetComponent<CarController>();
        m_CarHealth = GetComponent<Health>();
        m_Rigidbody = GetComponent<Rigidbody>();

        m_PlayerMainText.text = "grab items or grab the flag!";

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
            //case "RocketPickup":
            //    GetComponent<WeaponManager>().get_rocket();
            //    Destroy(other.transform.parent.transform.parent.gameObject);
            //    RocketDrops rocketDrops = GameObject.Find("Manager").GetComponent<RocketDrops>();
            //    rocketDrops.RocketPickedUp();
            //    break;
            //case "ShieldPickup":
            //    transform.Find("Shield").gameObject.SetActive(true);
            //    Destroy(other.gameObject);
            //    StartCoroutine(WaitToRespawn(other.gameObject));
            //    break;
            //case "LandminePickup":
                //GetComponent<WeaponManager>().get_landmine();
                //Destroy(other.gameObject);
                //break;

            case "TutCarBodyPickup":
                pickupLevel = other.GetComponent<PickupLevel>();
                m_CarBodyLevel = pickupLevel.m_PickupLevel;
                UpdateCarBody();

                m_PlayerMainText.text = "car body pickups increase player health!";
                StartCoroutine(TutorialText());
                Destroy(other.gameObject);
                break;
            case "TutEnginePickup":
                pickupLevel = other.GetComponent<PickupLevel>();
                m_EngineLevel = pickupLevel.m_PickupLevel;
                UpdateEngine();

                m_PlayerMainText.text = "engine pickups increase player max speed!";
                StartCoroutine(TutorialText());
                Destroy(other.gameObject);
                break;
            case "TutTirePickup":
                pickupLevel = other.GetComponent<PickupLevel>();
                m_TireLevel = pickupLevel.m_PickupLevel;
                UpdateTires();

                m_PlayerMainText.text = "tire pickups increase player traction!";
                StartCoroutine(TutorialText());
                Destroy(other.gameObject);
                break;
            case "TutFlag":
                m_TutorialDoor.SetActive(false);
                m_PlayerMainText.text = "exit the arena!";

                // TODO: fix OpenDoors
                //OpenDoors openDoors = m_TutorialDoor.GetComponent<OpenDoors>();
                //if (openDoors == null)
                //{
                //    Debug.LogError("ERROR: openDoors not set on m_TutorialDoor in PlayerPickup.cs");
                //}
                //openDoors.Open();

                Destroy(other.gameObject);
                break;
            case "TutFireRing":
                m_PlayerMainText.text = "ready!";
                m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                ReadyUpManager.instance.PlayersReadiedUp();
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

    private IEnumerator TutorialText()
    {
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(3);
        m_PlayerMainText.text = "";
        m_Rigidbody.constraints = RigidbodyConstraints.None;
    }
}