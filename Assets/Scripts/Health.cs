using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    
    public float health;
    public float maxHealth;
    public float damageMultrest;
    public float damageMultfront;
    public int body = 0;
    float damageMult;
    //bool attacking;
    Rigidbody carRb;
    Respawn respawn;
    Vector3 initialVel = Vector3.zero;
    Vector3 finalVel = Vector3.zero;
    // Use this for initialization
    void Start () {
        carRb = GetComponent<Rigidbody>();
        respawn = GetComponent<Respawn>();
        damageMult = damageMultrest;
    }
    
    // Update is called once per frame
    void Update () {
        if(health<=0){
            //gameObject.SetActive(false);
            health = 10;
            respawn.respawn();

        }
        if(health < ((body * 10))){
            body--;
        }
    }

    //Currently assuming we can't skip body upgrades
    public void changeBody(int i){
        if (body < i) { 
            health = 10*i + 10;
            maxHealth = 10 * i + 10;
            body = i;
        }
    }
    //Make sure the empty game objects can't collide with the car
    private void OnCollisionEnter(Collision collision)
    {
        initialVel = carRb.velocity;
        RaycastHit hit; //RaycastHit hit2;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.collider==collision.collider){
                Debug.Log("Point of contact is front");
                //if(collision.gameObject.tag == )
                damageMult = damageMultfront;
            }
            else
            {
                Debug.Log("Point of contact is not front");
                damageMult = damageMultrest;
            }

        }
        if(!collision.gameObject.CompareTag("nonObstacle")){
            StartCoroutine(damageEnum()); 
        }

        //if (Physics.Raycast(transform.position, (-transform.forward), out hit2))
        //{
        //    if (hit.collider == collision.collider)
        //    {
        //        Debug.Log("Point of contact is back");
        //    }
        //}
    }
  
    IEnumerator damageEnum()
    {
        yield return new WaitForSeconds(0.1f);
        finalVel = carRb.velocity;
        //Debug.Log(finalVel);
        health = health - Mathf.Abs(finalVel.magnitude - initialVel.magnitude) * damageMult;
        initialVel = Vector3.zero;
    }
    

}