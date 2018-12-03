using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    private RawImage Black;

    // Use this for initialization
    void Start () {
        Black = GetComponent<RawImage>();
        Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, 1f);
        StartCoroutine(In());
    }

    private IEnumerator In()
    {
        for (float i = 2f; i > 0f; i -= Time.deltaTime)
        {
            Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, i / 2);
            yield return null;
        }
        Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, 0f);
        Black.gameObject.SetActive(false);
    }
}
