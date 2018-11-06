using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public GameObject left = null;
    public GameObject right = null;
    public GameObject target;

    private float cooldown = 1;

	// Use this for initialization
	void Start () {

    }

    private void Update()
    {
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
    }

    public void get_rocket() {

    }

    public void fire() {
        if (left != null && cooldown <= 0.0f) {
            left.GetComponent<Rocket>().fire(target);
            left = null;
            cooldown = 1f;
        }
        else if (right != null && cooldown <= 0.0f)
        {
            right.GetComponent<Rocket>().fire(target);
            right = null;
            cooldown = 1f;
        }
    }
}
