using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour {

    private bool active;
    private float timer;
    public GameObject m_ExplosionPrefab;
    public AudioClip explode;
    public float exp_power = 10.0f;
    public float exp_radius = 10.0f;

	// Use this for initialization
	void Start () {
        active = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!active) {
            timer += Time.deltaTime;
            if (timer > 2.0f) {
                active = true;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (active) {
            if (other.tag == "Player" || other.tag == "Player2" || other.tag == "Player3" || other.tag == "Player4") {
                Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(explode, transform.position);
                Collider[] affected = Physics.OverlapSphere(transform.position, exp_radius);
                for (int i = 0; i < affected.Length; i++) {
                    if (affected[i].tag == "Player" || affected[i].tag == "Player2" || affected[i].tag == "Player3" || affected[i].tag == "Player4") {
                        affected[i].gameObject.GetComponent<Health>().AlterHealth(-5);
                        Rigidbody rb = affected[i].GetComponent<Rigidbody>();

                        if (rb != null)
                            rb.AddExplosionForce(exp_power, transform.position, exp_radius, 3.0F);
                    }
                }
                Destroy(this.gameObject);
            }
        } 
    }
}
