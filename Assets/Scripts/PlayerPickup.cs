using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour 
{
    public GameObject carBodyObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarBodyPickup")
        {
            carBodyObject.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.tag == "EnginePickup")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "TirePickup")
        {
            Destroy(other.gameObject);
        }
    }
}
