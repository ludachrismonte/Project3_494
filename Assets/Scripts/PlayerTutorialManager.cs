using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class PlayerTutorialManager : MonoBehaviour
{
    public Text m_PlayerMainText;
    public Text m_SubText;
    public GameObject m_ReadyImage;
    public GameObject m_TutorialDoor;
    public GameObject m_Flag;
    public PlayerNumber player;

    private Rigidbody m_Rigidbody;

    private string m_CurrentMessage;
    private bool m_HasMoved;
    private bool m_ReadiedUp;
    
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        m_CurrentMessage = "grab the flag!";
        m_PlayerMainText.text = "";

        m_ReadiedUp = false;
        m_HasMoved = false;
        m_Flag.SetActive(false);

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
        else if (other.tag == "TutFlag")
        {
            StartCoroutine(GameRulesText());
            m_CurrentMessage = "exit the arena!";
            m_Flag.SetActive(true);
            Destroy(other.gameObject);
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

    private IEnumerator GameRulesText()
    {
        m_PlayerMainText.text = "hold the flag to earn points! jump through fire hoops to earn extra points!";
        yield return new WaitForSeconds(4);
        m_PlayerMainText.text = "take the flag from other players by running " +
            "into them or by knocking them out!";
        yield return new WaitForSeconds(4);
        m_PlayerMainText.text = "";
        m_TutorialDoor.SetActive(false);
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