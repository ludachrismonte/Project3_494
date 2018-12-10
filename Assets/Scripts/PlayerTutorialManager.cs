using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class PlayerTutorialManager : MonoBehaviour
{
    public PickupLevelEnum m_CarBodyLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_TireLevel = PickupLevelEnum.one;
    public PickupLevelEnum m_EngineLevel = PickupLevelEnum.one;
    public Text m_PlayerMainText;
    public Text m_SubText;
    public GameObject m_ReadyImage;
    public RawImage[] m_Speedometers;
    public GameObject m_TutorialDoor;
    public GameObject m_Flag;
    public PlayerNumber player;
    public bool m_IsReadyUp;

    private CarController m_CarController;
    private Health m_CarHealth;
    private Rigidbody m_Rigidbody;
    private WeaponManager m_WeaponManager;

    private RawImage m_CurrentSpeedometer;
    private string m_CurrentMessage;
    private bool m_HasMoved = false;
    private bool m_ShowingText = false;

    private bool m_ReadiedUp;
    
    private void Start()
    {
        m_CarController = GetComponent<CarController>();
        m_CarHealth = GetComponent<Health>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_WeaponManager = GetComponent<WeaponManager>();

        m_CurrentMessage = m_IsReadyUp ? "grab the flag!" : "grab pickups or the flag!";
        m_PlayerMainText.text = "";

        m_ReadiedUp = false;

        foreach (RawImage speedometer in m_Speedometers)
            speedometer.enabled = false;

        m_CurrentSpeedometer = m_Speedometers[0];
        m_CurrentSpeedometer.enabled = true;

        m_Flag.SetActive(false);

        UpdateCarBody();
        UpdateEngine();
        UpdateTires();

        StartCoroutine(ControlsText());
        StartCoroutine(SwapSubText());
    }

    private void Update()
    {
        if (!m_HasMoved && m_Rigidbody.velocity.magnitude > 2.0f)
        {
            m_PlayerMainText.text = "";
            m_HasMoved = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Pickup(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        Pickup(other);
    }

    private void Pickup(Collider other)
    {
        if (other.tag == "TutFireRing")
        {
            ReadyUpManager.instance.PlayersReadiedUp(player, this);
            return;
        }

        if (m_ShowingText)
            return;

        PickupLevel pickupLevel;
        switch (other.tag)
        {
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
            case "TutRocketPickup":
                m_PlayerMainText.text = "rockets can be locked on and shot at other players by pressing b!";
                StartCoroutine(TutorialText());
                StartCoroutine(m_WeaponManager.TutorialRocket());
                Destroy(other.gameObject);
                break;
            case "TutShieldPickup":
                m_PlayerMainText.text = "shields protect you from rockets and landmines!";
                StartCoroutine(TutorialText());
                StartCoroutine(TutorialShield());
                Destroy(other.gameObject);
                break;
            case "TutLandminePickup":
                m_PlayerMainText.text = "landmines can be placed by pressing b!";
                StartCoroutine(TutorialText());
                StartCoroutine(m_WeaponManager.TutorialLandmine());
                Destroy(other.gameObject);
                break;
            case "TutFlag":
                StartCoroutine(GameRulesText());
                m_CurrentMessage = "exit the arena!";
                m_Flag.SetActive(true);
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

    private IEnumerator ControlsText()
    {
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        m_PlayerMainText.text = "press a to accelerate\n" +
            "press x to reverse";
        yield return new WaitForSeconds(2);
        m_Rigidbody.constraints = RigidbodyConstraints.None;
    }

    private IEnumerator TutorialText()
    {
        m_ShowingText = true;
        yield return new WaitForSeconds(4);
        m_PlayerMainText.text = "";
        m_ShowingText = false;
    }

    private IEnumerator GameRulesText()
    {
        m_ShowingText = true;
        m_PlayerMainText.text = "hold the flag to earn points! jump through fire hoops to earn extra points!";
        yield return new WaitForSeconds(4);
        m_PlayerMainText.text = "take the flag from other players by running " +
            "into them or by knocking them out!";
        yield return new WaitForSeconds(4);
        m_PlayerMainText.text = "";
        m_TutorialDoor.SetActive(false);
        m_ShowingText = false;
    }

    private IEnumerator TutorialShield()
    {
        transform.Find("Shield").GetComponent<Shield>().Activate();
        yield return new WaitForSeconds(4);
    }

    public void ReadyUp()
    {
        m_ReadiedUp = true;
        m_PlayerMainText.text = "ready!";
        m_ReadyImage.SetActive(true);
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private IEnumerator SwapSubText()
    {
        while (!m_ReadiedUp)
        {
            m_SubText.text = "click start to skip!";
            yield return new WaitForSeconds(2);
            m_SubText.text = m_CurrentMessage;
            yield return new WaitForSeconds(2);
        }
        m_SubText.text = "";
    }
}