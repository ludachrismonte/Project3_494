using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSwitcher : MonoBehaviour {

    public GameObject[] ctf_objects;

    private int index_active;

    private void Start()
    {
        for (int i = 0; i < ctf_objects.Length; i++) 
        {
            ctf_objects[i].SetActive(false);
        }
        index_active = (int)Random.Range(0, ctf_objects.Length);
        ctf_objects[index_active].SetActive(true);
    }

    public void Switch () 
    {
        if (ctf_objects[index_active].GetComponent<FireRing>())
        {
            ctf_objects[index_active].GetComponent<FireRing>().Deactivate();
        }
        else
        {
            ctf_objects[index_active].SetActive(false);
        }

        int temp = (int)Random.Range(0, ctf_objects.Length);
        while (temp == index_active) 
        {
            temp = (int)Random.Range(0, ctf_objects.Length);
        }

        index_active = temp;
        ctf_objects[index_active].SetActive(true);
        if (ctf_objects[index_active].GetComponent<FireRing>())
        {
            ctf_objects[index_active].GetComponent<FireRing>().Activate();
        }
    }

    public Transform Get_Active() 
    {
        return ctf_objects[index_active].transform;
    }

    public void Disable_Active()
    {
        ctf_objects[index_active].SetActive(false);
    }

    public void Enable_Active()
    {
        ctf_objects[index_active].SetActive(true);
    }
}
