using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlacement : MonoBehaviour {

    public int m_Radius;
    public GameObject[] m_Arrows;

    private void Start()
    {
        if (m_Arrows != null)
        {
            foreach (GameObject arrow in m_Arrows)
            {
                arrow.SetActive(true);
            }
        }
        else
        {
            GameObject player1 = GameObject.FindGameObjectWithTag("Player");
            GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
            GameObject player3 = GameObject.FindGameObjectWithTag("Player3");
            GameObject player4 = GameObject.FindGameObjectWithTag("Player4");

            player1.transform.Find("Arrow").gameObject.SetActive(true);
            player2.transform.Find("Arrow").gameObject.SetActive(true);
            player3.transform.Find("Arrow").gameObject.SetActive(true);
            player4.transform.Find("Arrow").gameObject.SetActive(true);
        }
    }

    public void Move()
    {
        Vector2 pos = Random.insideUnitCircle * m_Radius;
        Vector3 newpos = new Vector3(pos[0], 0, pos[1]);
        transform.position = newpos;
    }

}
