using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCorder : MonoBehaviour {

    public float speed;

	void Start () {
		
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 rot = new Vector3(-1, 0, 0);
            transform.Rotate(rot * speed * Time.deltaTime / 2);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 rot = new Vector3(0, -1, 0);
            transform.Rotate(rot * speed * Time.deltaTime / 2);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 rot = new Vector3(1, 0, 0);
            transform.Rotate(rot * speed * Time.deltaTime / 2);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 rot = new Vector3(0, 1, 0);
            transform.Rotate(rot * speed * Time.deltaTime / 2);
        }
    }
}
