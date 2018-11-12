using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {

    public GameObject left = null;
    public GameObject right = null;
    public MeshRenderer targeter = null;
    public GameObject shooter;
    public GameObject projectile;
    public Text text;

    private bool has_left = false;
    private bool has_right = false;
    private GameObject target;
    private float cooldown = 1;

	// Use this for initialization
	void Start () {
        text.text = "";
        right.SetActive(false);
        left.SetActive(false);
        shooter.GetComponent<MeshRenderer>().enabled = false;
        if (targeter) { targeter.enabled = false; }
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
            if (target.tag == "Player") { text.text = "targeting: blue"; text.color = Color.cyan; }
            else if (target.tag == "Player2") { text.text = "targeting: red"; text.color = Color.red; }
            else if (target.tag == "Player3") { text.text = "targeting: green"; text.color = Color.green; }
            else if (target.tag == "Player4") { text.text = "targeting: yellow"; text.color = Color.yellow; }

        }
        if (!has_left && !has_right) { text.text = ""; }
    }

    public void get_rocket() {
        shooter.GetComponent<MeshRenderer>().enabled = true;
        if (!has_left && !has_right) {
            StartCoroutine(raise());
        }
        has_left = true;
        left.SetActive(true);
        has_right = true;
        right.SetActive(true);
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
        if (targeter) {
            targeter.enabled = true;
        }
        for (float i = .6f; i < 1.5f; i += .05f) {
            shooter.transform.localPosition += new Vector3(0, .05f, 0);
            yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator lower()
    {
        if (targeter)
        {
            targeter.enabled = false;
        }
        yield return new WaitForSeconds(.3f);
        for (float i = .6f; i < 1.5f; i += .05f)
        {
            shooter.transform.localPosition -= new Vector3(0, .05f, 0);
            yield return new WaitForSeconds(0);
        }
        shooter.GetComponent<MeshRenderer>().enabled = false;
    }
}
