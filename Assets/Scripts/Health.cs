using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    
    public float health;
    public float damageMultrest;
    public float damageMultfront;
    public Image health_bar;
    public GameObject cam;

    private Vector3 origin;
    private Vector3 cam_origin;
    private float damageMult;
    private Rigidbody carRb;
    private Respawn respawn;
    private Vector3 initialVel = Vector3.zero;
    private Vector3 finalVel = Vector3.zero;
    private PlayerPickup m_PlayerPickup;

    void Start () 
    {
        origin = gameObject.transform.position;
        cam_origin = cam.transform.position;
        carRb = GetComponent<Rigidbody>();
        respawn = GetComponent<Respawn>();
        m_PlayerPickup = GetComponent<PlayerPickup>();

        damageMult = damageMultrest;
    }
    
    void Update () 
    {
        health_bar.fillAmount = health / 50;
        if(health <= 0)
        {
            gameObject.transform.position = origin;
            cam.transform.position = cam_origin;
        }
    }

    //Make sure the empty game objects can't collide with the car

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
    }
  
    IEnumerator damageEnum()
    {
        yield return new WaitForSeconds(0.1f);
        finalVel = carRb.velocity;
        health -= Mathf.Abs(finalVel.magnitude - initialVel.magnitude) * damageMult;
        initialVel = Vector3.zero;
    }

    public void take_damage(float amt) {
        health -= amt;
    }
}
