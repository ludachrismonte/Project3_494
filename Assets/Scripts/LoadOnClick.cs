using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadOnClick : MonoBehaviour 
{
    public RawImage black;
    public string scene = "quit";

    private void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        if (scene == "ReadyUp")
        {
            btn.Select();
        }
        btn.onClick.AddListener(LoadScene);
    }

    public void LoadScene()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade() 
    {
        black.gameObject.SetActive(true);
        for (float i = 0; i < 1; i += Time.deltaTime) 
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, i);
            yield return null;
        }
        if (scene != "quit") 
        {
            SceneManager.LoadScene(scene); 
        }
        else 
        {
            Application.Quit();
        }
    }

}
