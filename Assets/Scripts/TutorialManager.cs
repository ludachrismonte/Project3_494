using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject Pickups;
    public GameObject Gate;
    public GameObject Players;
    public float scene_rate;

    private float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
        Pickups.SetActive(false);
        Gate.SetActive(false);
        Players.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer > 0 && timer < scene_rate) {
            Pickups.SetActive(true);
            return;
        }
        Pickups.SetActive(false);
        if (timer > scene_rate && timer < scene_rate * 2)
        {
            Gate.SetActive(true);
            return;
        }
        Gate.SetActive(false);
        if (timer > scene_rate * 2)
        {
            Players.SetActive(true);
            Destroy(this);
        }
    }
}
