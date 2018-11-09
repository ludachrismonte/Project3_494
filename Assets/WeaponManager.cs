using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public GameObject left = null;
    public GameObject right = null;
    private bool has_left = false;
    private bool has_right = false;
    private GameObject target;
    public GameObject shooter;

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
        target = GetComponent<ControllerInput>().getTargeted();
        if (target != null)
        {
            shooter.transform.LookAt(target.transform);
        }
    }

    public void get_rocket() {
        Debug.Log("got a rocket");
        if (!has_left && !has_right) {
            StartCoroutine(raise());
        }
        if (!has_left)
        {
            Debug.Log("act left");
            has_left = true;
            left.SetActive(true);
        }
        else {
            Debug.Log("act right");
            has_right = true;
            right.SetActive(true);
        }
    }

    public void fire() {
        if (has_left && cooldown <= 0.0f) {
            if (target != null) {
                GameObject bullet = Instantiate(projectile, left.transform.position, left.transform.rotation) as GameObject;
                bullet.GetComponent<Rocket>().SetTarget(target);
                has_left = false;
                left.SetActive(false);
                cooldown = 1f;
            }
            if (!has_left && !has_right) { StartCoroutine(lower()); }
        }
        else if (has_right && cooldown <= 0.0f)
        {
            if (target != null)
            {
                GameObject bullet = Instantiate(projectile, right.transform.position, right.transform.rotation) as GameObject;
                bullet.GetComponent<Rocket>().SetTarget(target);
                has_right = false;
                right.SetActive(false);
                cooldown = 1f;
            }
            if (!has_left && !has_right) { StartCoroutine(lower()); }
        }
    }

    private IEnumerator raise() {
        for (float i = .6f; i < 1.5f; i += .05f) {
            shooter.transform.position += new Vector3(0, .05f, 0);
            yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator lower()
    {
        yield return new WaitForSeconds(.3f);
        for (float i = .6f; i < 1.5f; i += .05f)
        {
            shooter.transform.position -= new Vector3(0, .05f, 0);
            yield return new WaitForSeconds(0);
        }
    }
}
