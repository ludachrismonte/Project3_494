using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketTargeter : MonoBehaviour 
{
    public GameObject[] m_OtherPlayers;
    public readonly float m_TargetingRange = 100f;
    public Camera m_PlayerCamera;
    public Image m_Crosshairs;

    private WeaponManager m_WeaponManager;
    private GameObject m_CurrentTarget;

	private void Start () 
    {
        m_Crosshairs.gameObject.SetActive(false);
        m_WeaponManager = GetComponent<WeaponManager>();
        m_CurrentTarget = null;
	}
	
	private void Update () 
    {
		if (m_WeaponManager.GetCurrentWeapon() == WeaponType.rocket)
        {
            GameObject target = null;
            float closestDistToTarget = -1f;
            foreach (GameObject player in m_OtherPlayers)
            {
                Transform otherPlayer = player.transform;

                Vector3 targetDirection = otherPlayer.position - transform.position;
                float angle = Mathf.Abs(Vector3.Angle(targetDirection, m_PlayerCamera.transform.forward));

                if (angle < 45)
                {
                    float distToOtherPlayer = Vector3.Distance(transform.position, otherPlayer.position);
                    if (distToOtherPlayer <= m_TargetingRange &&
                        (Mathf.Approximately(closestDistToTarget, -1) || 
                         distToOtherPlayer < closestDistToTarget))
                    {
                        target = player;
                        closestDistToTarget = distToOtherPlayer;
                    }
                }
            }
            m_CurrentTarget = target;

            if (m_CurrentTarget != null)
            {
                m_Crosshairs.gameObject.SetActive(true);
                m_Crosshairs.transform.position = m_PlayerCamera.WorldToScreenPoint(m_CurrentTarget.transform.position);

                switch(m_CurrentTarget.tag)
                {
                    case "Player":
                        m_Crosshairs.color = Color.blue;
                        break;
                    case "Player2":
                        m_Crosshairs.color = Color.red;
                        break;
                    case "Player3":
                        m_Crosshairs.color = Color.green;
                        break;
                    case "Player4":
                        m_Crosshairs.color = Color.yellow;
                        break;
                }
            }
            else
            {
                m_Crosshairs.gameObject.SetActive(false);
            }
        }
        else
        {
            m_Crosshairs.gameObject.SetActive(false);
            m_CurrentTarget = null;
        }
	}

    public GameObject GetTarget()
    {
        return m_CurrentTarget;
    }
}
