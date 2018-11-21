using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour 
{
    private Transform m_Target;

    void LateUpdate()
    {
        if (m_Target != null && m_Target.gameObject.activeSelf)
            transform.LookAt(m_Target);
    }

    public void SetTarget(Transform target)
    {
        m_Target = target;
    }
}

