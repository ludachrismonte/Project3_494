using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRingManager : MonoBehaviour 
{
    public GameObject[] m_FireRings;
    public int m_FireRingsToSpawn;

    private Dictionary<GameObject, bool> m_ActiveFireRings;
    private int m_NumberOfActive;

	private void Start () 
    {
        m_ActiveFireRings = new Dictionary<GameObject, bool>();

        foreach (GameObject fireRing in m_FireRings)
        {
            fireRing.SetActive(false);
            m_ActiveFireRings.Add(fireRing, false);
        }
        print(m_ActiveFireRings);
        m_NumberOfActive = 0;
        SpawnFireRings();
	}

    public IEnumerator FireRingHit(GameObject fireRingObject)
    {

        --m_NumberOfActive;
        m_ActiveFireRings[fireRingObject] = false;
        fireRingObject.GetComponent<FireRing>().Deactivate();
        yield return new WaitForSeconds(1);
        fireRingObject.SetActive(false);
        SpawnFireRings();
    }

    private void SpawnFireRings()
    {
        while (m_NumberOfActive < m_FireRingsToSpawn && m_NumberOfActive < m_FireRings.Length)
        {
            int newFireRing = Random.Range(0, m_FireRings.Length);
            if (m_ActiveFireRings[m_FireRings[newFireRing]] == false)
            {
                m_FireRings[newFireRing].SetActive(true);
                m_FireRings[newFireRing].GetComponent<FireRing>().Activate();
                m_ActiveFireRings[m_FireRings[newFireRing]] = true;
                ++m_NumberOfActive;
            }
        }
        print(m_ActiveFireRings);
    }
}
