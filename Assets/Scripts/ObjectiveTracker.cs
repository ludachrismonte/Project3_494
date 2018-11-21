using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlagHolder { none, p1, p2, p3, p4 };

public class ObjectiveTracker : MonoBehaviour 
{
    public RingSwitcher m_Flags;
    public RingSwitcher m_Rings;

    public GameObject[] Arrows;

    private FlagHolder m_FlagHolder;
    
	void Start() 
    {
        m_FlagHolder = FlagHolder.none;
        foreach (GameObject arrow in Arrows)
        {
            arrow.SetActive(false);
        }
	}
	
	void Update()
    {
		if (m_FlagHolder == FlagHolder.none)
        {
            foreach (GameObject arrow in Arrows)
            {
                LookAtObject lookAtObject = arrow.GetComponent<LookAtObject>();
                lookAtObject.SetTarget(m_Flags.Get_Active());
            }
        }
	}

    public void SetFlagHolder(FlagHolder newFlagHolder)
    {
        m_FlagHolder = newFlagHolder;
    }
}
