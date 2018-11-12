using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDrops : MonoBehaviour 
{
    public GameObject drop;
    public int m_MaxDrops = 15;
    public float m_TimeDelay = 10;

    private int m_CurrentDrops = 0;
    private float m_Timer;

	private void Start () {
        Spawn();
        Spawn();
        Spawn();
        Spawn();
    }

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
        if (m_CurrentDrops <= m_MaxDrops)
        {
            Vector2 pos = Random.insideUnitCircle * 220;
            Vector3 newpos = new Vector3(pos[0], 30, pos[1]);
            Instantiate(drop, newpos, Quaternion.identity);
            ++m_CurrentDrops;
        }
    }

    public void DropPickedUp()
    {
        --m_CurrentDrops;
    }
}
