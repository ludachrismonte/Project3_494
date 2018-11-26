using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamage : MonoBehaviour {
	
	// Update is called once per frame
	void Update () 
    {
        if (transform.position.y < 2.5) 
        {
            gameObject.GetComponent<Health>().AlterHealth(-.5f);
        }
	}
}
