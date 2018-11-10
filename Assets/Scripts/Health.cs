using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    
    public float health;
    public float maxHealth;
    public float damageMultrest;
    public float damageMultfront;

    private float damageMult;
    private Rigidbody carRb;
    private Respawn respawn;
    private Vector3 initialVel = Vector3.zero;
    private Vector3 finalVel = Vector3.zero;
    private PlayerPickup m_PlayerPickup;

    void Start () 
    {
        carRb = GetComponent<Rigidbody>();
        respawn = GetComponent<Respawn>();
        m_PlayerPickup = GetComponent<PlayerPickup>();

        damageMult = damageMultrest;
    }
    
    void Update () 
    {
        // TODO: reimplment health

        //if(health <= 0)
        //{
        //    health = 10;
        //    respawn.respawn();
        //    return;
        //}
    }

<<<<<<< HEAD
    //Currently assuming we can't skip body upgrades
    public void changeBody(int i){
        if (body < i) { 
            health = 10*i + 10;
            maxHealth = 10 * i + 10;
            body = i;
        }
    }
    //Make sure the empty game objects can't collide with the car
=======
>>>>>>> 24d95925563071d6b11f0836c9c7bffc890f1f24
    private void OnCollisionEnter(Collision collision)
    {
        initialVel = carRb.velocity;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.collider == collision.collider)
            {
                Debug.Log("Point of contact is front");
                damageMult = damageMultfront;
            }
            else
            {
                Debug.Log("Point of contact is not front");
                damageMult = damageMultrest;
            }

        }

        if(collision.gameObject.tag != "nonObstacle")
        {
            StartCoroutine(damageEnum()); 
        }
    }
  
    IEnumerator damageEnum()
    {
        yield return new WaitForSeconds(0.1f);
        finalVel = carRb.velocity;
        health -= Mathf.Abs(finalVel.magnitude - initialVel.magnitude) * damageMult;
        initialVel = Vector3.zero;
    }
}
