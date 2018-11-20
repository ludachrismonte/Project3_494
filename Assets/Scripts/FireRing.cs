using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRing : MonoBehaviour {

    private Vector3 init_scale;

    private AudioSource whoosh;

	// Use this for initialization
	void Start () 
    {
        whoosh = GetComponent<AudioSource>();
        init_scale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate() {
        whoosh.Play();
        StartCoroutine(grow());
    }

    public void Deactivate()
    {
        whoosh.Play();
        StartCoroutine(shrink());
    }

    private IEnumerator grow() {
        transform.localScale = Vector3.zero;
        for (float i = 0; i < .75; i += Time.deltaTime)
        {
            transform.localScale = init_scale * Mathf.Sin(i * Mathf.PI / 2) / 0.75f;
            yield return null;
        }
    }

    private IEnumerator shrink()
    {
        transform.localScale = Vector3.zero;
        for (float i = 0; i < .75; i += Time.deltaTime)
        {
            transform.localScale = init_scale * (0.75f - Mathf.Sin(i * Mathf.PI / 2)) / 0.75f;
            yield return null;
        }
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
