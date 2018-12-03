using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Credits : MonoBehaviour
{

    public float speed = 120f;
    private Rigidbody stuff;
    private RectTransform position;
    public RawImage Black;

    // Use this for initialization
    void Start()
    {
        stuff = GetComponent<Rigidbody>();
        position = GetComponent<RectTransform>();
        Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, 1f);
        StartCoroutine(In());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stuff.velocity = new Vector3(0f, 1f, 0f) * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish") {
            StartCoroutine(Out());
        }
    }

    private IEnumerator In()
    {
        for (float i = 2f; i > 0f; i -= Time.deltaTime)
        {
            Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, i / 2);
            yield return null;
        }
        Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, 0f);
    }

    private IEnumerator Out()
    {
        yield return new WaitForSeconds(1);
        for (float i = 2f; i > 0f; i -= Time.deltaTime)
        {
            Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, 1 - (i / 2));
            yield return null;
        }
        Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, 1f);
        SceneManager.LoadScene("Menu");
    }
}