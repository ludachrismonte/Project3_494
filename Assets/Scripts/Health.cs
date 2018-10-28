using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    
    public float health;
    public float damageMult;
    Rigidbody carRb;
    Vector3 initialVel = Vector3.zero;
    Vector3 finalVel = Vector3.zero;
	// Use this for initialization
	void Start () {
        carRb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    //Make sure the empty game objects can't collide with the car
    private void OnCollisionEnter(Collision collision)
    {
        initialVel = carRb.velocity;
        StartCoroutine(damageEnum());
    }
  
    IEnumerator damageEnum()
    {
        yield return new WaitForSeconds(0.1f);
        finalVel = carRb.velocity;
        Debug.Log(finalVel);
        health = health - Mathf.Abs(finalVel.magnitude - initialVel.magnitude) * damageMult;
        initialVel = Vector3.zero;
    }
    

}
