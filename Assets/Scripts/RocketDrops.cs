﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDrops : MonoBehaviour 
{
    public GameObject m_RocketPickup;
    public float m_Radius = 180;
    public int m_MaxDrops = 5;
    public float m_TimeDelay = 30;
    public float m_HeightOfDrop;

    private int m_CurrentDrops;
    private float m_Timer;

    private void Update () 
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > m_TimeDelay) 
        {
            Spawn();
            m_Timer = 0;
        }
    }

    private void Spawn() 
    {
        if (m_CurrentDrops < m_MaxDrops)
        {
            Vector2 pos = Random.insideUnitCircle * m_Radius;
            Vector3 newpos = new Vector3(pos.x, m_HeightOfDrop, pos.y);
            Instantiate(m_RocketPickup, newpos, Quaternion.identity);
            ++m_CurrentDrops;
        }
    }

    public void RocketPickedUp()
    {
        --m_CurrentDrops;
    }
}
