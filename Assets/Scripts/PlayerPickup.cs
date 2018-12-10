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
    public GameObject[] m_CarTires; // list of lists
    public Camera cam;

    public GameObject UpgradesFolder;
    private GameObject Speed5;
    private GameObject Speed4;
    private GameObject Speed3;
    private GameObject Speed2;
    private GameObject Health5;
    private GameObject Health4;
    private GameObject Health3;
    private GameObject Health2;
    private GameObject Traction5;
    private GameObject Traction4;
    private GameObject Traction3;
    private GameObject Traction2;

    private CarController m_CarController;
    private Health m_CarHealth;
    private WeaponManager m_WeaponManager;

    private RawImage m_CurrentSpeedometer;
    private RawImage m_NewSpeedometer;
    private PickupLevelEnum m_CurrentTireLevel;

    private void Start()
    {
        SetUpgrades();
        m_CarController = GetComponent<CarController>();
        m_CarHealth = GetComponent<Health>();
        m_WeaponManager = GetComponent<WeaponManager>();

        foreach (RawImage speedometer in m_Speedometers)
            speedometer.enabled = false;

        m_CurrentSpeedometer = m_Speedometers[0];

        foreach (GameObject tires in m_CarTires)
        {
            for (int i = 0; i < tires.transform.childCount; ++i)
            {
                tires.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        m_CurrentTireLevel = m_TireLevel;

        UpdateCarBody(transform.position);
        UpdateEngine(transform.position);
        UpdateTires(transform.position);
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
                    UpdateCarBody(other.transform.position);
                    StartCoroutine(WaitToRespawn(other.gameObject));
                }
                break;
            case "EnginePickup":
                pickupLevel = other.GetComponent<PickupLevel>();
                if (pickupLevel.m_PickupLevel > m_EngineLevel)
                {
                    m_EngineLevel = pickupLevel.m_PickupLevel;
                    UpdateEngine(other.transform.position);
                    StartCoroutine(WaitToRespawn(other.gameObject));
                }
                break;
            case "TirePickup":
                pickupLevel = other.GetComponent<PickupLevel>();
                if (pickupLevel.m_PickupLevel > m_TireLevel)
                {
                    m_TireLevel = pickupLevel.m_PickupLevel;
                    UpdateTires(other.transform.position);
                    StartCoroutine(WaitToRespawn(other.gameObject));
                }
                break;
            case "RocketPickup":
                if (m_WeaponManager.GetCurrentWeapon() == WeaponType.none)
                {
                    m_WeaponManager.EquipRocket();
                    Destroy(other.gameObject);
                }
                else if (m_WeaponManager.GetCurrentWeapon() == WeaponType.rocket && !m_WeaponManager.HasTwoRockets)
                {
                    m_WeaponManager.ReEquipRockets();
                    Destroy(other.gameObject);
                }
                break;
            case "ShieldPickup":
                transform.Find("Shield").gameObject.GetComponent<Shield>().Activate();
                Destroy(other.gameObject);
                break;
            case "LandminePickup":
                if (m_WeaponManager.GetCurrentWeapon() == WeaponType.none)
                {
                    m_WeaponManager.EquipLandmine();
                    Destroy(other.gameObject);
                }
                break;
        }
    }

    private void UpdateCarBody(Vector3 collision_origin)
    {
        switch (m_CarBodyLevel)
        {
            case PickupLevelEnum.two:
                StartCoroutine(UpgradeJuice(collision_origin, Health2));
                m_CarHealth.SetHealth(20);
                break;
            case PickupLevelEnum.three:
                StartCoroutine(UpgradeJuice(collision_origin, Health3));
                m_CarHealth.SetHealth(30);
                break;
            case PickupLevelEnum.four:
                StartCoroutine(UpgradeJuice(collision_origin, Health4));
                m_CarHealth.SetHealth(40);
                break;
            case PickupLevelEnum.five:
                StartCoroutine(UpgradeJuice(collision_origin, Health5));
                m_CarHealth.SetHealth(50);
                break;
            default:
                m_CarHealth.SetHealth(10);
                break;
        }
    }

    private void UpdateEngine(Vector3 collision_origin)
    {
        switch (m_EngineLevel)
        {
            case PickupLevelEnum.two:
                m_CarController.MaxSpeed = 75;
                m_NewSpeedometer = m_Speedometers[1];
                StartCoroutine(UpgradeJuice(collision_origin, Speed2));
                StartCoroutine(SpeedometerJuice(collision_origin));
                break;
            case PickupLevelEnum.three:
                m_CarController.MaxSpeed = 100;
                m_NewSpeedometer = m_Speedometers[2];
                StartCoroutine(UpgradeJuice(collision_origin, Speed3));
                StartCoroutine(SpeedometerJuice(collision_origin));
                break;
            case PickupLevelEnum.four:
                m_CarController.MaxSpeed = 125;
                m_NewSpeedometer = m_Speedometers[3];
                StartCoroutine(UpgradeJuice(collision_origin, Speed4));
                StartCoroutine(SpeedometerJuice(collision_origin));
                break;
            case PickupLevelEnum.five:
                m_CarController.MaxSpeed = 150;
                m_NewSpeedometer = m_Speedometers[4];
                StartCoroutine(UpgradeJuice(collision_origin, Speed5));
                StartCoroutine(SpeedometerJuice(collision_origin));
                break;
            default:
                m_CarController.MaxSpeed = 50;
                m_CurrentSpeedometer.enabled = false;
                m_CurrentSpeedometer = m_Speedometers[0];
                m_CurrentSpeedometer.enabled = true;
                break;
        }
    }

    private void UpdateTires(Vector3 collision_origin)
    {
        foreach (GameObject tires in m_CarTires)
        {
            tires.transform.GetChild((int)m_CurrentTireLevel - 1).gameObject.SetActive(false);
        }
        m_CurrentTireLevel = m_TireLevel;

        switch (m_TireLevel)
        {
            case PickupLevelEnum.two:
                StartCoroutine(UpgradeJuice(collision_origin, Traction2));
                m_CarController.SteerHelperValue = 0.7f;
                break;
            case PickupLevelEnum.three:
                StartCoroutine(UpgradeJuice(collision_origin, Traction3));
                m_CarController.SteerHelperValue = 0.8f;
                break;
            case PickupLevelEnum.four:
                StartCoroutine(UpgradeJuice(collision_origin, Traction4));
                m_CarController.SteerHelperValue = 0.9f;
                break;
            case PickupLevelEnum.five:
                StartCoroutine(UpgradeJuice(collision_origin, Traction5));
                m_CarController.SteerHelperValue = 1.0f;
                break;
            default:
                m_CarController.SteerHelperValue = 0.6f;
                break;
        }

        StartCoroutine(Grow());

    }

    private IEnumerator Grow()
    {
        foreach (GameObject tires in m_CarTires)
        {
            tires.transform.GetChild((int)m_CurrentTireLevel - 1).gameObject.SetActive(true);
        }
        Vector3 initial = m_CarTires[0].transform.GetChild((int)m_CurrentTireLevel - 1).gameObject.transform.localScale;
        for (float i = 0f; i < 0.5f; i += Time.deltaTime)
        {
            foreach (GameObject tires in m_CarTires)
            {
                tires.transform.GetChild((int)m_CurrentTireLevel - 1).gameObject.transform.localScale = initial * (.5f + i);
            }
            yield return null;
        }
        foreach (GameObject tires in m_CarTires)
        {
            tires.transform.GetChild((int)m_CurrentTireLevel - 1).gameObject.transform.localScale = initial;
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
        m_WeaponManager.UnEquipRockets();
        m_WeaponManager.UnequipLandmine();
        transform.Find("Shield").GetComponent<Shield>().Deactivate();

        m_CarBodyLevel = PickupLevelEnum.one;
        UpdateCarBody(transform.position);
        m_TireLevel = PickupLevelEnum.one;
        UpdateTires(transform.position);
        m_EngineLevel = PickupLevelEnum.one;
        UpdateEngine(transform.position);
    }

    private IEnumerator SpeedometerJuice(Vector3 collision_origin) 
    {
        m_NewSpeedometer.enabled = true;
        Vector3 end_position = m_NewSpeedometer.transform.position;
        Vector3 pos = cam.WorldToScreenPoint(collision_origin);
        float counter = 0.0f;
        float duration = 0.75f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            m_NewSpeedometer.transform.localScale = Vector3.Lerp(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1f, 1f, 1f), counter / duration);
            m_NewSpeedometer.transform.position = Vector3.Lerp(pos, end_position, counter / duration);
            yield return null;
        }
        m_CurrentSpeedometer.enabled = false;
        m_CurrentSpeedometer = m_NewSpeedometer;
    }

    private IEnumerator UpgradeJuice(Vector3 collision_origin, GameObject obj)
    {
        obj.SetActive(true);
        Vector3 end_position = obj.transform.position;
        Vector3 pos = cam.WorldToScreenPoint(collision_origin);
        float counter = 0.0f;
        float duration = 0.75f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            obj.transform.localScale = Vector3.Lerp(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1f, 1f, 1f), counter / duration);
            obj.transform.position = Vector3.Lerp(pos, end_position, counter / duration);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        Image img = obj.GetComponent<Image>();
        for (float i = .5f; i > 0f; i -= Time.deltaTime)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, i * 2);
            yield return null;
        }
        obj.SetActive(false);
    }

    private void SetUpgrades() {
        Speed5 = UpgradesFolder.transform.Find("Speed5").gameObject;
        Speed4 = UpgradesFolder.transform.Find("Speed4").gameObject;
        Speed3 = UpgradesFolder.transform.Find("Speed3").gameObject;
        Speed2 = UpgradesFolder.transform.Find("Speed2").gameObject;
        Traction5 = UpgradesFolder.transform.Find("Traction5").gameObject;
        Traction4 = UpgradesFolder.transform.Find("Traction4").gameObject;
        Traction3 = UpgradesFolder.transform.Find("Traction3").gameObject;
        Traction2 = UpgradesFolder.transform.Find("Traction2").gameObject;
        Health5 = UpgradesFolder.transform.Find("Health5").gameObject;
        Health4 = UpgradesFolder.transform.Find("Health4").gameObject;
        Health3 = UpgradesFolder.transform.Find("Health3").gameObject;
        Health2 = UpgradesFolder.transform.Find("Health2").gameObject;

        Speed5.SetActive(false);
        Speed4.SetActive(false);
        Speed3.SetActive(false);
        Speed2.SetActive(false);
        Traction5.SetActive(false);
        Traction4.SetActive(false);
        Traction3.SetActive(false);
        Traction2.SetActive(false);
        Health5.SetActive(false);
        Health4.SetActive(false);
        Health3.SetActive(false);
        Health2.SetActive(false);
    }
}