using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlacement : MonoBehaviour {

    public int radius;

    public void Move()
    {
        Debug.Log("hit");
        Vector2 pos = Random.insideUnitCircle * radius;
        Vector3 newpos = new Vector3(pos[0], 0, pos[1]);
        transform.position = newpos;
    }

}
