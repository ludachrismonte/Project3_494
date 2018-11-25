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
    public GameObject UIObjects;

    public GameObject m_TutorialDoor;

    private CarController m_CarController;
    private Health m_CarHealth;
    private Rigidbody m_Rigidbody;

    private RawImage spedometer_1;
    private RawImage spedometer_2;
    private RawImage spedometer_3;
    private RawImage spedometer_4;
    private RawImage spedometer_5;

    private Text m_PlayerMainText;

    private void Start()
    {
        if (UIObjects == null)
        {
            Debug.LogError("ERROR: UIObjects not set in PlayerPickup.cs");
        }
        else if (m_TutorialDoor == null)
        {
            Debug.LogError("ERROR: m_TutorialDoor not set in PlayerPickup.cs");
        }

        m_CarController = GetComponent<CarController>();
        m_CarHealth = GetComponent<Health>();
        m_Rigidbody = GetComponent<Rigidbody>();

        // TODO: fix this, this is not effiecient... use a public variable array
        spedometer_1 = UIObjects.transform.Find("spedometer_1").gameObject.GetComponent<RawImage>();
        spedometer_2 = UIObjects.transform.Find("spedometer_2").gameObject.GetComponent<RawImage>();
        spedometer_3 = UIObjects.transform.Find("spedometer_3").gameObject.GetComponent<RawImage>();
        spedometer_4 = UIObjects.transform.Find("spedometer_4").gameObject.GetComponent<RawImage>();
        spedometer_5 = UIObjects.transform.Find("spedometer_5").gameObject.GetComponent<RawImage>();
        m_PlayerMainText = UIObjects.transform.Find("MainText").gameObject.GetComponent<Text>();

        m_PlayerMainText.text = "";

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
                GetComponent<WeaponManager>().get_rocket();
                Destroy(other.transform.parent.transform.parent.gameObject);
                RocketDrops rocketDrops = GameObject.Find("Manager").GetComponent<RocketDrops>();
                rocketDrops.RocketPickedUp();
                break;
            case "ShieldPickup":
                transform.Find("Shield").gameObject.SetActive(true);
                Destroy(other.gameObject);
                StartCoroutine(WaitToRespawn(other.gameObject));
                break;
            case "LandminePickup":
                GetComponent<WeaponManager>().get_landmine();
                Destroy(other.gameObject);
                break;
            case "TutCarBodyPickup":
                m_PlayerMainText.text = "car body pickups increase player health!";
                StartCoroutine(TutorialText());
                Destroy(other.gameObject);
                break;
            case "TutEnginePickup":
                m_PlayerMainText.text = "engine pickups increase player max speed!";
                StartCoroutine(TutorialText());
                Destroy(other.gameObject);
                break;
            case "TutTirePickup":
                m_PlayerMainText.text = "tire pickups increase player traction!";
                StartCoroutine(TutorialText());
                Destroy(other.gameObject);
                break;
            case "TutFlag":
                // TODO: fix OpenDoors
                m_TutorialDoor.SetActive(false);
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
                GameManager.instance.PlayerReady();
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

    private IEnumerator TutorialText()
    {
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(3);
        m_PlayerMainText.text = "";
        m_Rigidbody.constraints = RigidbodyConstraints.None;
    }
}