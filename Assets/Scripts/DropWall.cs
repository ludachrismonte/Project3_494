using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWall : MonoBehaviour {

    public float counter = 20;
	
	// Update is called once per frame
	void Update () {
        counter -= Time.deltaTime;
        Debug.Log(counter);
        if (counter < 0) {
            transform.Translate(new Vector3(0, 0, 5) * Time.deltaTime);
        }
        if (counter < -10)
        {
            Destroy(this.gameObject);
        }
    }
}
