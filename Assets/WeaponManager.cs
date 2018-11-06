using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public GameObject left = null;
    public GameObject right = null;
    private bool has_left = false;
    private bool has_right = false;
    public GameObject target;

    public GameObject projectile;

    private float cooldown = 1;

	// Use this for initialization
	void Start () {
        right.SetActive(false);
        left.SetActive(false);
    }

    private void Update()
    {
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
    }

    public void get_rocket() {
        if (!has_left)
        {
            has_left = true;
            left.SetActive(true);
        }
        else {
            has_right = true;
            right.SetActive(true);
        }
    }

    public void fire() {
        if (has_left && cooldown <= 0.0f) {
            GameObject bullet;
            bullet = Instantiate(projectile, left.transform.position, left.transform.rotation) as GameObject;
            bullet.GetComponent<Rocket>().SetTarget(target);
            has_left = false;
            left.SetActive(false);
            cooldown = 1f;
        }
        else if (has_right && cooldown <= 0.0f)
        {
            GameObject bullet = Instantiate(projectile, right.transform.position, right.transform.rotation) as GameObject;
            bullet.GetComponent<Rocket>().SetTarget(target);
            has_right = false;
            right.SetActive(false);
            cooldown = 1f;
        }
    }
}
