using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDrops : MonoBehaviour {

    private float time_delay = 5;
    private float timer;
    public GameObject drop;

	void Start () {
        Spawn();
        Spawn();
        Spawn();
        Spawn();
    }

    void Update () {
        timer += Time.deltaTime;
        if (timer > time_delay) {
            Spawn();
            timer = 0.0f;
        }
    }

    void Spawn() {
        Vector2 pos = Random.insideUnitCircle * 220;
        Vector3 newpos = new Vector3(pos[0], 30, pos[1]);
        Instantiate(drop, newpos, Quaternion.identity);
    }
}
