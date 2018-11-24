using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour 
{
    public GameObject[] pickups;

    private GameObject parachute;
    private GameObject closed_crate;
    private GameObject open_crate;
    private bool open;

	void Start () 
    {
        parachute = transform.Find("parachute").gameObject;
        closed_crate = transform.Find("closed_crate").gameObject;
        open_crate = transform.Find("open_crate").gameObject;
        open = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!open) 
        {
            open = true;
            closed_crate.SetActive(false);
            parachute.SetActive(false);
            open_crate.SetActive(true);
            int spawn = (int)Random.Range(0, pickups.Length);
            Instantiate(pickups[spawn], open_crate.transform.position, Quaternion.identity);
        }
    }
}
