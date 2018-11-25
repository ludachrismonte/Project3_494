using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour 
{
    public readonly float m_TimeLimit = 90f;

    private DropSpawner m_DropManager;
    private float m_Timer;

	private void Start () 
    {
        m_DropManager = GameManager.instance.GetComponent<DropSpawner>();
        m_Timer = 0;
	}
	
	private void Update () 
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > m_TimeLimit || transform.position.y <= -2.5)
        {
            Destroy(gameObject);
        }
	}

    private void OnDestroy()
    {
        m_DropManager.DropPickedUp();
    }
}
