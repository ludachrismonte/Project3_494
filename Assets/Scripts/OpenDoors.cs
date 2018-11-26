using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour 
{
    private Transform m_RightDoor;
    private Transform m_LeftDoor;

    private void Start()
    {
        m_RightDoor = transform.Find("Right");
        m_LeftDoor = transform.Find("Left"); 
    }

    public void Open() 
    {
        StartCoroutine(OpenDoor(m_RightDoor, 1));
        StartCoroutine(OpenDoor(m_LeftDoor, -1));
    }

    private IEnumerator OpenDoor(Transform door, int direction)
    {
        for (float i = 0.0f; i < 4; i += Time.deltaTime)
        {
            door.Rotate(direction * Vector3.forward, 30f * Time.deltaTime);
            yield return null;
        }
    }
}
