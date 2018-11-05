using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    
    public float health;
    public float damageMultrest;
    public float damageMultfront;
<<<<<<< HEAD

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
        if(health <= 0)
        {
            health = 10;
            respawn.respawn();
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        initialVel = carRb.velocity;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.collider == collision.collider)
            {
                Debug.Log("Point of contact is front");
=======
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
            health += 10;
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
>>>>>>> parent of 6fac596... Merge branch 'master' of https://github.com/ludachrismonte/Project3_494
                damageMult = damageMultfront;
            }
            else
            {
                Debug.Log("Point of contact is not front");
                damageMult = damageMultrest;
            }

        }
<<<<<<< HEAD

        if(collision.gameObject.tag != "nonObstacle")
        {
            StartCoroutine(damageEnum()); 
        }
=======
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
>>>>>>> parent of 6fac596... Merge branch 'master' of https://github.com/ludachrismonte/Project3_494
    }
  
    IEnumerator damageEnum()
    {
        yield return new WaitForSeconds(0.1f);
        finalVel = carRb.velocity;
<<<<<<< HEAD
        health -= Mathf.Abs(finalVel.magnitude - initialVel.magnitude) * damageMult;
        initialVel = Vector3.zero;
    }
}
=======
        //Debug.Log(finalVel);
        health = health - Mathf.Abs(finalVel.magnitude - initialVel.magnitude) * damageMult;
        initialVel = Vector3.zero;
    }
    

}
>>>>>>> parent of 6fac596... Merge branch 'master' of https://github.com/ludachrismonte/Project3_494
