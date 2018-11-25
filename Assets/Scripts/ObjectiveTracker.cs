using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlagHolder { none, p1, p2, p3, p4 };

public class ObjectiveTracker : MonoBehaviour { 

    private FlagHolder m_FlagHolder;
    
	void Start() 
    {
        m_FlagHolder = FlagHolder.none;
	}

    private void Update()
    {
        Debug.Log("current: " + m_FlagHolder);
    }

    public void SetFlagHolder(FlagHolder newFlagHolder)
    {
        m_FlagHolder = newFlagHolder;
    }

    public FlagHolder getFlagHolder()
    {
        return m_FlagHolder;
    }
}
