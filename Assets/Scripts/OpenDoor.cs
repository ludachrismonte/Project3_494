using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    public int reverse = 1;

    void Start () {
	}
	
	public void Open () {
        StartCoroutine(open());
    }

    private IEnumerator open() {
        for (float i = 0.0f; i < 4; i += Time.deltaTime) {
            transform.Rotate(reverse * Vector3.forward, 30f * Time.deltaTime);
            yield return null;
        }
    }
}
