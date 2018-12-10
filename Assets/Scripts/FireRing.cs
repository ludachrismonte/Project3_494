using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRing : MonoBehaviour {

    private Vector3 m_InitScale;
    private AudioSource m_WhooshSound;

    public void Activate() 
    {
        m_InitScale = transform.localScale;
        m_WhooshSound = GetComponent<AudioSource>();
        m_WhooshSound.Play();
        StartCoroutine(Grow());
    }

    public void Deactivate()
    {
        m_WhooshSound.Play();
        StartCoroutine(Shrink());
    }

    private IEnumerator Grow() {
        transform.localScale = Vector3.zero;
        for (float i = 0; i < .75; i += Time.deltaTime)
        {
            transform.localScale = m_InitScale * Mathf.Sin(i * Mathf.PI / 2) / 0.75f;
            yield return null;
        }
    }

    private IEnumerator Shrink()
    {
        transform.localScale = Vector3.zero;
        for (float i = 0; i < .75; i += Time.deltaTime)
        {
            transform.localScale = m_InitScale * (0.75f - Mathf.Sin(i * Mathf.PI / 2)) / 0.75f;
            yield return null;
        }
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
