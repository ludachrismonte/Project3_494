using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour 
{
    public GameObject m_DropBox;
    public float m_Radius = 180;
    public int m_MaxDrops = 10;
    public float m_HeightOfDrop;

    private int m_CurrentDrops;

    private void Update () 
    {
        if (m_CurrentDrops < m_MaxDrops)
        {
            Spawn();
        }
    }

    private void Spawn() 
    {
        Vector2 pos = Random.insideUnitCircle * m_Radius;
        Vector3 newpos = new Vector3(pos.x, m_HeightOfDrop, pos.y);
        Instantiate(m_DropBox, newpos, Quaternion.identity);
        ++m_CurrentDrops;
    }

    public void DropPickedUp()
    {
        --m_CurrentDrops;
    }
}
