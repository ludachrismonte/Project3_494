using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionFeedback : MonoBehaviour {
    //public static float damage = 0;
    public float damageMult;
    public GameObject car;
    Rigidbody carRb;
    Vector3 initialVel;
    Vector3 finalVel;
    Health health;
	// Use this for initialization
	void Start () {
        carRb = car.GetComponent<Rigidbody>();
        health = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    //Make sure the empty game objects can't collide with the car
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        initialVel = carRb.velocity;
    }
    //private void OnCollisionExit(Collision collision)
    //{
        
    //}

    IEnumerator damageEnum(){
        
        yield return new WaitForSeconds(1f);
        finalVel = carRb.velocity;
        health.health = health.health - Mathf.Abs(finalVel.magnitude - initialVel.magnitude) * damageMult;
        initialVel = Vector3.zero;
    }
}
