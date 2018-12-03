using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ShowLogo : MonoBehaviour {

    private RawImage Logo;

	// Use this for initialization
	void Start () {
        Logo = transform.Find("Logo").GetComponent<RawImage>();
        Logo.color = new Color(Logo.color.r, Logo.color.g, Logo.color.b, 0f);
        StartCoroutine(Play());
    }

    private IEnumerator Play() {
        yield return new WaitForSeconds(1);
        for (float i = 0f; i < 1f; i += Time.deltaTime)
        {
            Logo.color = new Color(Logo.color.r, Logo.color.g, Logo.color.b, i);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        for (float i = 1f; i > 0f; i -= Time.deltaTime)
        {
            Logo.color = new Color(Logo.color.r, Logo.color.g, Logo.color.b, i);
            yield return null;
        }
        Logo.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }
}
