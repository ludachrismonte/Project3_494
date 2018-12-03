using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour 
{
    public GameObject[] m_Flags;

    private int m_ActiveIndex;

	void Start () 
    {
        foreach (GameObject flag in m_Flags)
            flag.SetActive(false);

        m_ActiveIndex = Random.Range(0, m_Flags.Length);
        m_Flags[m_ActiveIndex].SetActive(true);
	}

    public Transform Get_Active()
    {
        return m_Flags[m_ActiveIndex].transform;
    }

    public void Disable_Active()
    {
        m_Flags[m_ActiveIndex].SetActive(false);
    }

    public void Enable_Active()
    {
        m_Flags[m_ActiveIndex].SetActive(true);
    }
}
