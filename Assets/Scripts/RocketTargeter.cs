using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTargeter : MonoBehaviour 
{
    public GameObject[] m_OtherPlayers;
    public readonly float m_TargetingRange = 100f;
    public Camera m_PlayerCamera;

    private WeaponManager m_WeaponManager;
    private GameObject m_CurrentTarget;

	private void Start () 
    {
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
        }
        else
        {
            m_CurrentTarget = null;
        }
	}

    public GameObject GetTarget()
    {
        return m_CurrentTarget;
    }
}
