using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadyUpManager : MonoBehaviour 
{
    public static ReadyUpManager instance;

    private int m_PlayersReady;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start () 
    {
        m_PlayersReady = 0;
	}

    public void PlayersReadiedUp()
    {
        ++m_PlayersReady;
        if (m_PlayersReady == 4)
        {
            StartCoroutine(OpenArean());
        }
    }

    private IEnumerator OpenArean()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Arena");
    }
}
